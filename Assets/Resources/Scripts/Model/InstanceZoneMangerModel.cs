using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using Utils;

/// <summary>
/// InstanceZoneManger的model
/// </summary>
public static class InstanceZoneMangerModel 
{
    public static List<InstanceZoneInfo> GetInstanceZoneInfoItems() {
        //获取本地档案Json
        TextAsset zoneTextAsset = Resources.Load<TextAsset>(Config.INSTANCEZONEINFO_JSON_PATH);
        //json转化成list
        return JsonUtils.Json2Class<InstanceZoneInfoItems>(zoneTextAsset.text).instanceZoneInfoList;
    }
}
