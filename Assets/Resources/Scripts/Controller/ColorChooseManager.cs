using System.Collections;
using System.Collections.Generic;
using Info;
using Interface;
using UnityEngine;
using Utils;

public class ColorChooseManager : BaseMonoBehaviour,INotifier
{
    #region 属性
    //Item预制体
    private GameObject ColorChooseItemGameObject;
    //所有Item的信息
    private List<ColorChooseInfo> colorChooseInfoList;
    //所有Item的controller控件，作为发布者
    private List<IObserver> colorChooseControllerList;
    //单例
    public static ColorChooseManager instance;
    //目前正在使用的颜色255范围的
    private Color currentChooseColor_255;
    #endregion

    protected override void Awake() {
        base.Awake();
        instance = this;
    }
    
    protected override void FetchComponent() {
        
    }

    protected override void GetResources() {
        colorChooseControllerList = new List<IObserver>();
        //获取颜色选择信息的json数据，和item预制体
        ColorChooseItemGameObject = Resources.Load<GameObject>(Config.COLOR_CHOOSE_ITEM_PREFAB_PATH);
        TextAsset colorInfoTextAsset = Resources.Load<TextAsset>(Config.COLORCHOOSEINFO_JSON_PATH);
        colorChooseInfoList = JsonUtils.Json2Class<ColorChooseInfoItems>(colorInfoTextAsset.text).colorChooseInfoList;
    }

    protected override void InitZoneLayout() {
        //初始化colorchoose
        for (int i = 0; i < colorChooseInfoList.Count; i++) {
            GameObject colorChooseItem = Instantiate(ColorChooseItemGameObject,this.transform);
            transform.transform.localScale = Vector3.one;
            ColorChooseController controller = colorChooseItem.GetComponent<ColorChooseController>();
            controller.SetColorChooseInfo(colorChooseInfoList[i]);
            //添加观察者
            AddObserver(controller);
        }
        //默认刚进来就是点击第0个
        if (colorChooseInfoList.Count!=0) {
            var colorChooseController = colorChooseControllerList[0] as ColorChooseController;
            Dictionary<string, object> message = new Dictionary<string, object>();
            message.Add("color",colorChooseController.colorInfo.color);
            message.Add("onehot",colorChooseController.colorInfo.onehot);
            NotifyObserver(message);
        }
    }
    
    # region 观察者设计模式
    public void AddObserver(IObserver observer) {
        colorChooseControllerList.Add(observer);
    }

    public void RemoveObserver(IObserver observer) {
        colorChooseControllerList.Remove(observer);
    }
    /// <summary>
    /// 通知所有颜色按钮，颜色改变了，如果传入的onehot是这个颜色的onehot，这个颜色就加深
    /// </summary>
    public void NotifyObserver(Dictionary<string, object> message) {
        Color color = message["color"] is Color ? (Color)message["color"] : default;
        currentChooseColor_255 = color;
        foreach (var colorChooseController in colorChooseControllerList) {
            colorChooseController.Notify(message);
        }
        //通知maskdraw颜色改变了
        MaskDrawManager.instance.penColour = new Color(currentChooseColor_255.r/255f,currentChooseColor_255.g/255f,currentChooseColor_255.b/255f,1);
    }

    # endregion
}
