using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : BaseMonoBehaviour {
    # region 组件
    //回退键
    private Button backButton;
    //教学键
    private Button teachButton;
    # endregion
    protected override void FetchComponent() {
        // delegateNameTexture = this.transform.Find("ColorName").GetComponent<Text>();
        backButton = this.transform.Find("Back/Image Button").GetComponent<Button>();
        teachButton = this.transform.Find("Teach/Image Button").GetComponent<Button>();
        backButton.onClick.AddListener(BackButtonListener);
        teachButton.onClick.AddListener(TeachButtonListener);
    }

    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }
    
    # region 监听

    private void BackButtonListener() {
        SceneDataManager.GetInstance().ToNewScene("MainScene");
    }

    private void TeachButtonListener() {
        //todo:加入教学
    }
    
    # endregion

}
