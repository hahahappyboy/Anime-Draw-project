using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 界面跳转时数据传输类
/// </summary>
public class SceneDataManager : BaseSingleTon<SceneDataManager>
{
    //传输数据
    private Dictionary<string, object> sceneOneshotData = null;
    //选择的档案
    private InstanceZoneInfo zoneInfo;
    

    //设置数据
    private void WriteSceneData(Dictionary<string, object> data) {
        if (sceneOneshotData != null) {
            Debug.LogError("切换数据不为空，上一次切换场景的数据没有被读取");
        }
        sceneOneshotData = data;
        zoneInfo = (InstanceZoneInfo)sceneOneshotData["instanceZoneInfo"];
    }
    //取出数据
    public Dictionary<string, object> ReadSceneData() {
        Dictionary<string, object> tempData = sceneOneshotData;
        sceneOneshotData = null;//清空
        return tempData;
    }
    //前往新场景
    public void ToNewScene(string sceneName, Dictionary<string, object> param = null) {
        //写入数据
        this.WriteSceneData(param);
        //加载新场景
        SceneManager.LoadScene(sceneName);
    }
    
    
    /// <summary>
    /// 获取当前选择档案的mask的json path
    /// </summary>
    /// <returns></returns>
    public string GetMaskJsonPath() {
        switch (zoneInfo.zoneID) {
            case "10001":
                return Config.MASKCHOOSEINFO_FIVE_JSON_PATH;
            case "10002":
                return Config.MASKCHOOSEINFO_DARLING_JSON_PATH;
            default:
                Debug.LogError("MASK的JSON路径错误");
                return "";
        }
    }
    /// <summary>
    /// 获取当前选择档案的角色json path
    /// </summary>
    /// <returns></returns>
    public string GetCharacterChooseJsonPath() {
        switch (zoneInfo.zoneID) {
            case "10001":
                return Config.CHARACTERCHOOSEINFO_FIVE_JSON_PATH;
            case "10002":
                return Config.CHARACTERCHOOSEINFO_DARLING_JSON_PATH;
            default:
                Debug.LogError("角色选择的JSON路径错误");
                return "";
        }
    }
    /// <summary>
    /// 选择模型
    /// </summary>
    /// <returns></returns>
    public string GetModelID() {
        switch (zoneInfo.zoneID) {
            case "10001":
                return Config.MODEL_ID_FIVE;
            case "10002":
                return Config.MODEL_ID_DARLING;
            default:
                Debug.LogError("MODEL_ID选择错误");
                return "";
        }
    }
    

}
