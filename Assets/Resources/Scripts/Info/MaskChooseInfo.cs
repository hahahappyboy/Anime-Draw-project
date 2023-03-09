using System.Collections.Generic;

namespace Info {
    /// <summary>
    /// mask基本信息
    /// </summary>
    [System.Serializable]
    public class MaskChooseInfo {
        //角色ID=characterChooseInfo里的id
        public int characterID;
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