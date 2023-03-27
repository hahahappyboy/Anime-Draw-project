import torch.nn as nn
import models.norms as norms
import torch
import torch.nn.functional as F
from torch.nn import init
import copy

class Generator(nn.Module):
    __instance = None
    model_id = None
    @classmethod
    def getInstance(self,opt):
        if not self.__instance or not self.model_id or self.model_id != opt.model_id:
            print('创建实例')
            self.model_id = opt.model_id
            netG = Generator(opt)
            def init_weights(m, gain=0.02):
                classname = m.__class__.__name__
                if classname.find('BatchNorm2d') != -1:
                    if hasattr(m, 'weight') and m.weight is not None:
                        init.normal_(m.weight.data, 1.0, gain)
                    if hasattr(m, 'bias') and m.bias is not None:
                        init.constant_(m.bias.data, 0.0)
                elif hasattr(m, 'weight') and (classname.find('Conv') != -1 or classname.find('Linear') != -1):
                    init.xavier_normal_(m.weight.data, gain=gain)
                    if hasattr(m, 'bias') and m.bias is not None:
                        init.constant_(m.bias.data, 0.0)

            netG.apply(init_weights)
            netEMA = copy.deepcopy(netG)
            netEMA.load_state_dict(torch.load(opt.checkpoints_path))
            self.__instance = netEMA

        return self.__instance



    def __init__(self, opt):
        super().__init__()
        self.opt = opt
        """const_input_class"""
        self.const_inputs = []
        self.const_inputs = torch.load(opt.const_input_path)

        """onnx"""
        ch = 64
        self.channels = [16*ch, 16*ch, 16*ch, 8*ch, 4*ch, 2*ch, 1*ch]
        """onnx"""
        self.init_W, self.init_H = 16,16
        self.conv_img = nn.Conv2d(self.channels[-1], 3, 3, padding=1)
        self.up = nn.Upsample(scale_factor=2)
        self.body = nn.ModuleList([])
        for i in range(len(self.channels)-1):
            self.body.append(ResnetBlock_with_SPADE(self.channels[i], self.channels[i+1], opt))
        """onnx"""
        self.fc = nn.Conv2d(self.opt.semantic_nc + self.opt.z_dim, 16 * ch, 3, padding=1)


    def compute_latent_vector_size(self, opt):
        w = opt.crop_size // (2**(opt.num_res_blocks-1))
        h = round(w / opt.aspect_ratio)
        return h, w
    """const_input_class"""
    def forward(self, input,input_class, z=None):
        seg = input
        """onnx"""
        # seg.cuda()
        """onnx"""
        # input_const = self.const_inputs[input_class]
        # seg = torch.cat((input_const, seg), dim=1)
        dev = "cpu"
        """const_input_class"""
        input_const = self.const_inputs[input_class]
        input_const = input_const.to("cpu")
        seg = torch.cat((input_const, seg), dim=1)

        x = F.interpolate(seg, size=(self.init_W, self.init_H))
        x = self.fc(x)
        """onnx"""
        for i in range(6):
            x = self.body[i](x, seg)
            """onnx"""
            if i < 6-1:
                x = self.up(x)
        x = self.conv_img(F.leaky_relu(x, 2e-1))
        x = F.tanh(x)
        return x


class ResnetBlock_with_SPADE(nn.Module):
    def __init__(self, fin, fout, opt):
        super().__init__()
        self.opt = opt
        self.learned_shortcut = (fin != fout)
        fmiddle = min(fin, fout)
        sp_norm = norms.get_spectral_norm(opt)
        self.conv_0 = sp_norm(nn.Conv2d(fin, fmiddle, kernel_size=3, padding=1))
        self.conv_1 = sp_norm(nn.Conv2d(fmiddle, fout, kernel_size=3, padding=1))
        if self.learned_shortcut:
            self.conv_s = sp_norm(nn.Conv2d(fin, fout, kernel_size=1, bias=False))

        spade_conditional_input_dims = opt.semantic_nc
        """onnx"""
        spade_conditional_input_dims += opt.z_dim

        self.norm_0 = norms.SPADE(opt, fin, spade_conditional_input_dims)
        self.norm_1 = norms.SPADE(opt, fmiddle, spade_conditional_input_dims)
        if self.learned_shortcut:
            self.norm_s = norms.SPADE(opt, fin, spade_conditional_input_dims)
        self.activ = nn.LeakyReLU(0.2, inplace=True)

    def forward(self, x, seg):
        if self.learned_shortcut:
            x_s = self.conv_s(self.norm_s(x, seg))
        else:
            x_s = x
        dx = self.conv_0(self.activ(self.norm_0(x, seg)))
        dx = self.conv_1(self.activ(self.norm_1(dx, seg)))
        out = x_s + dx
        return out
