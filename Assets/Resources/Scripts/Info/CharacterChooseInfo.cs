using System.Collections.Generic;
using UnityEngine;

namespace Info {
    /// <summary>
    /// 角色选择信息
    /// </summary>
    [System.Serializable]
    public class CharacterChooseInfo {
        //角色名字
        public string characterName;
       //角色图片的URL
       public string characterImageURL;

    }
    
    [System.Serializable]
    public class CharacterChooseInfoItems {
        public List<CharacterChooseInfo> colorChooseInfoList;
    }
}