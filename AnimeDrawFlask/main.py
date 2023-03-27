from flask import Flask
from flask import request, make_response
import torch
from PIL import Image
from torchvision import transforms as TR
import numpy as np
import os
import cv2
import config
from models.generator import Generator
app = Flask(__name__, static_folder='./result')


@app.route('/',methods=['POST','GET'])
def めんま():
    cdir = os.getcwd()+"\\image\\めんま.jpg"
    image_data = open(cdir, 'rb').read()
    res = make_response(image_data)
    res.headers['Content-Type'] = 'image/png'
    return res

def color_convert(img_path,save_path):
    im = cv2.imread(img_path)
    im = cv2.cvtColor(im, cv2.COLOR_BGR2RGB)
    # 一定要先转背景，因为背景为0。而one-hot编码后背景也为0
    im[np.all(im == (0, 0, 0), axis=-1)] = (10 ,10, 10)
    im[np.all(im == (64, 0, 128), axis=-1)] = (0 ,0, 0)
    im[np.all(im == (128, 128, 0), axis=-1)] = (7, 7, 7)
    im[np.all(im == (0, 0, 128), axis=-1)] = (5, 5, 5)
    im[np.all(im == (128, 0, 128), axis=-1)] = (9, 9, 9)
    im[np.all(im == (0, 128, 128), axis=-1)] = (4, 4, 4)
    im[np.all(im == (128, 128, 128), axis=-1)] = (1 ,1, 1)
    im[np.all(im == (64, 0, 0), axis=-1)] = (2 ,2, 2)
    im[np.all(im == (192, 0, 0), axis=-1)] = (3 ,3, 3)
    im[np.all(im == (64, 128, 0), axis=-1)] = (6 ,6, 6)
    im[np.all(im == (192, 128, 0), axis=-1)] = (8 ,8, 8)
    # im[np.where(im == (0, 0, 0))] = (10 ,10, 10)
    r, g, b = cv2.split(im)
    cv2.imwrite(save_path, r)

"""图像预处理"""
def preprocess_label(label,opt):
    label = TR.functional.resize(label, (512, 512), Image.NEAREST)
    label = TR.functional.to_tensor(label)
    label = label * 255
    label = label.unsqueeze(0)
    label = label.long()
    label_map = label
    bs, _, h, w = label_map.size()
    nc = opt.semantic_nc
    input_label = torch.FloatTensor(bs, nc, h, w).zero_()
    input_semantics = input_label.scatter_(1, label_map, 1.0)
    cls = opt.label_class
    img_class = torch.tensor(int(cls))
    img_class = img_class.unsqueeze(0)
    return input_semantics,img_class
"""tensor转image"""
def tens_to_im(tens):
    out = (tens + 1) / 2
    out.clamp(0, 1)
    return np.transpose(out.detach().cpu().numpy(), (1, 2, 0))
"""保存图像"""
def save_im( im,path):
    im = Image.fromarray(im.astype(np.uint8))
    im.save(path)
@app.route('/AnimeDraw',methods=['POST','GET'])
def AnimeDraw():
    SAVE_IMG_PATH = os.path.abspath('.') + '\\result\image.png'
    SAVE_LABEL_PATH = os.path.abspath('.')+'\\result\label.png'
    SAVE_LABEL_ONEHOT_PATH  = os.path.abspath('.')+'\\result\onehot.png'
    # label图像
    label_img = request.files.get('label_img')
    # label角色的类别
    label_class = request.form.get('label_class')
    # 用哪个模型
    model_id = request.form.get('model_id')

    print("model_id="+model_id)
    print("label_class="+label_class)

    label_img.save(SAVE_LABEL_PATH)
    color_convert(img_path=SAVE_LABEL_PATH,save_path=SAVE_LABEL_ONEHOT_PATH)
    label = Image.open(SAVE_LABEL_ONEHOT_PATH)

    if model_id == 'five':
        opt = config.read_arguments_five()
        opt.label_class = label_class
        netEMA = Generator.getInstance(opt)
    elif model_id == 'darling':
        opt = config.read_arguments_darling()
        opt.label_class = label_class
        netEMA = Generator.getInstance(opt)

    input_semantics,img_class = preprocess_label(label,opt)
    fake = netEMA(input_semantics, img_class)
    im = tens_to_im(fake[0]) * 255
    save_im(im, SAVE_IMG_PATH)

    return '/result/image.png'


if __name__ == '__main__':
    app.run(host='0.0.0.0')