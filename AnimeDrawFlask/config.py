import argparse

def read_arguments_five():
    parser = argparse.ArgumentParser()
    opt = parser.parse_args()
    opt.z_dim = 64  # ConstInput维度
    opt.class_num = 9  # 角色总类
    opt.semantic_nc = 11  # 语义图维度
    opt.batch_size = 1
    opt.checkpoints_path = './checkpoints/five.pth'
    opt.const_input_path = './checkpoints/const_input_five.pt'
    opt.gpu_ids = "-1"
    opt.model_id = "five"
    return opt

def read_arguments_darling():
    parser = argparse.ArgumentParser()
    opt = parser.parse_args()
    opt.z_dim = 64  # ConstInput维度
    opt.class_num = 9  # 角色总类
    opt.semantic_nc = 11  # 语义图维度
    opt.batch_size = 1
    opt.checkpoints_path = './checkpoints/darling.pth'
    opt.const_input_path = './checkpoints/const_input_darling.pt'
    opt.gpu_ids = "-1"
    opt.model_id = "darling"
    return opt

