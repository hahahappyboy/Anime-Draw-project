using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaskChooseController : BaseMonoBehaviour,IPointerDownHandler
{
    #region 组件
    private Image maskBGImage;
    private Image maskImage;
    //maskImage的纹理图像
    private Texture2D texture2D;
    
    #endregion
    
    # region 属性

    public MaskChooseInfo maskChooseInfo; 
    //第一次检测的时间
    [Header("双击间隔时间")]
    public float intervalTime = 0.3f;
    //第一次点击的时间
    private float clickTime = 0;
    
    # endregion
    
    protected override void FetchComponent() {
        maskBGImage = this.GetComponent<Image>();
        maskImage = this.transform.Find("MaskImage").GetComponent<Image>();
        
    }
    /// <summary>
    /// 设置mask信息
    /// </summary>
    /// <param name="info"></param>
    public void SetMaskChooseInfo(MaskChooseInfo info) {
        maskChooseInfo = info;
        Sprite sprite = Resources.Load<Sprite>(maskChooseInfo.maskImageURL);
        texture2D = sprite.texture;
        maskImage.sprite = sprite;
        this.transform.localScale = Vector3.one;
    }
    
    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }

  

    public void OnPointerDown(PointerEventData eventData) {
        //Time.time 为本次点击的时间
        if (Time.time - clickTime <= intervalTime)//双击
        {
            MaskDrawManager.instance.SetTexture2D((Color32[])texture2D.GetPixels32().Clone()); 
        }else
        {
            // Debug.Log("时间过长不认为双击");
            clickTime = Time.time;//上一次点击的时间
        }
    
    }
}
