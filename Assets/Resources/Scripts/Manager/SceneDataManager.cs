using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 界面跳转时数据传输类
/// </summary>
public class SceneDataManager : BaseSingleTon<SceneDataManager>
{
    //传输数据
    private Dictionary<string, object> sceneOneshotData = null;
    
    
    //设置数据
    private void WriteSceneData(Dictionary<string, object> data) {
        if (sceneOneshotData != null) {
            Debug.LogError("切换数据不为空，上一次切换场景的数据没有被读取");
        }
        sceneOneshotData = data;
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

}
