using System.Collections.Generic;

namespace Info {
    /// <summary>
    /// mask基本信息
    /// </summary>
    [System.Serializable]
    public class MaskChooseInfo {
        //人物ID
        public string characterID;
       //人物名
       public string maskName;
       //MaskURL
       public string maskImageURL;

    }
    
    [System.Serializable]
    public class MaskChooseInfoItems {
        public List<MaskChooseInfo> maskChooseInfoList;
    }
}