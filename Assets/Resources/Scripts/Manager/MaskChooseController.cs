using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.UI;

public class MaskChooseController : BaseMonoBehaviour
{
    #region 组件
    private Image maskBGImage;
    private Image maskImage;
    #endregion
    
    # region 属性

    public MaskChooseInfo maskChooseInfo; 
    
    #endregion
    
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
        maskImage.sprite = sprite;
    }
    
    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }

   
}
