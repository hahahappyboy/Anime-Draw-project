using System.Collections.Generic;
using UnityEngine;

namespace Info {
    /// <summary>
    /// 颜色选择信息，头发、眼睛。。。。。。
    /// </summary>
    [System.Serializable]
    public class ColorChooseInfo {
        //颜色对应的名字
        public string delegateName;
        //颜色
        public int r;
        public int g;
        public int b;
        public Color color;
        //ont-hot颜色
        public int onehot;
    }
    
    [System.Serializable]
    public class ColorChooseInfoItems {
        public List<ColorChooseInfo> colorChooseInfoList;
    }
}