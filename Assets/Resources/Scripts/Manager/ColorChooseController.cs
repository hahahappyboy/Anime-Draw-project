using System.Collections;
using System.Collections.Generic;
using Info;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorChooseController : BaseMonoBehaviour,IPointerClickHandler,IObserver
{
    # region 组件
    //颜色对应名字
    public Text delegateNameTexture;
    //背景Image
    public Image ColorBgImage;
    //框Image
    public Image colorFrameImage;
    //它的RGB
    public ColorChooseInfo colorInfo;
    # endregion

    #region 属性
    //背景透明度
    private const float BG_TRANSPARENT = 90;
    
    #endregion
    
    
    protected override void FetchComponent() {
        delegateNameTexture = this.transform.Find("ColorName").GetComponent<Text>();
        ColorBgImage = this.transform.Find("ColorBG").GetComponent<Image>();
        colorFrameImage = this.transform.Find("ColorFrame").GetComponent<Image>();
    }

    public void SetColorChooseInfo(ColorChooseInfo colorInfo) {
        this.colorInfo = colorInfo;
        Color color = new Color(colorInfo.r/255f, colorInfo.g/255f, colorInfo.b/255f,BG_TRANSPARENT/255f);
        colorInfo.color = new Color(colorInfo.r, colorInfo.g, colorInfo.b,255f);
        delegateNameTexture.text = colorInfo.delegateName;
        ColorBgImage.color = color;
        colorFrameImage.color = color;
    }
    
    
    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
       
    }

    /// <summary>
    /// 观察者，被通知颜色改变了，查看是不是自己颜色改变了
    /// </summary>
    /// <param name="onehot"></param>
    public void Notify(Dictionary<string, object> message) {
        int onehot = message["onehot"] is int ? (int)message["onehot"] : 0;
        if (colorInfo.onehot == onehot) {
            ColorBgImage.color = new Color(colorInfo.r/255f, colorInfo.g/255f, colorInfo.b/255f,1);
        } else {
            ColorBgImage.color = new Color(colorInfo.r/255f, colorInfo.g/255f, colorInfo.b/255f,BG_TRANSPARENT/255f);
        }
    }
    /// <summary>
    /// 鼠标点击事件，通知其他colorchoose颜色改变了
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("color",colorInfo.color);
        message.Add("onehot",colorInfo.onehot);
        ColorChooseManager.instance.NotifyObserver(message);
    }

   
}
