using System.Collections.Generic;

namespace Info {
    [System.Serializable]
    public class InstanceZoneInfo {
        //档案Image的地址
        public string zoneImageURL;
        //档案的中文信息
        public string zoneTextCN;
        //档案的日文信息
        public string zoneTextJP;
        //档案的编号 
        public string zoneTextID;
        //动画URL
        public string zoneVideoeURL;
        //可生成的人物 人物名
        public List<string> zoneGenPeopelName;
        //是否锁住
        public bool isLock;
        

    }
    
    [System.Serializable]
    public class InstanceZoneInfoItems {
        public List<InstanceZoneInfo> instanceZoneInfoList;
    }
}