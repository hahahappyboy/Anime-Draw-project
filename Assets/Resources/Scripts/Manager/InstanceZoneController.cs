using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 用于控制每个档案的
/// </summary>
public class InstanceZoneController : BaseMonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {
    # region 组件
    [Header("档案的图片")] 
    [SerializeField]
    private Image _zoneImage;
    [Header("档案中文信息")]
    [SerializeField]
    private Text _zoneTextCN;
    [Header("档案日文信息")]
    [SerializeField]
    private Text _ZoneTextJP;
    //档案锁，表示档案是否可用
    private GameObject _lockGameObject;
    //当鼠标移到档案，显示的编辑GameObject
    private GameObject _drawGameObject;
    # endregion
    
    # region 属性
    //档案是否被锁住
    private bool isLock = false;
    //文档的信息
    private InstanceZoneInfo _instanceZoneInfo;
    # endregion

    protected override void Awake() {
       base.Awake();
    }
    protected override void Start() {
       base.Start();
    }
    
    /// <summary>
    /// 档案是否具备被显示的条件
    /// </summary>
    private bool CanDisplay() {
        if (_zoneImage.sprite == null) return false;
        if (_zoneTextCN.text == null) return false;
        if (_ZoneTextJP.text == null) return false;
        return true;
    }
    
    /// <summary>
    /// 获取档案组件
    /// </summary>
    protected override void FetchComponent() {
        _zoneImage = this.transform.Find("Image").GetComponent<Image>();
        _lockGameObject = this.transform.Find("Image/Lock").gameObject;
        _drawGameObject = this.transform.Find("Image/Draw").gameObject;
        _zoneTextCN = this.transform.Find("TextBG/Text CN").GetComponent<Text>();
        _ZoneTextJP = this.transform.Find("TextBG/Text JP").GetComponent<Text>();
    }
    /// <summary>
    /// 获取资源
    /// </summary>
    protected override void GetResources() {
        
    }
    /// <summary>
    /// 初始化界面
    /// </summary>
    protected override void InitZoneLayout() {
        //查看界面能否被初始化
        if (!CanDisplay()) Debug.LogWarning("有档案无法被初始化");
        //是否显示被锁住的按钮
        if (isLock) {
            _lockGameObject.SetActive(true);
        } else {
            _lockGameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 设置档案的图片，中文信息，日文信息
    /// </summary>
    /// <param name="zoneImage">档案的图片</param>
    /// <param name="zoneStrCN">中文信息</param>
    /// <param name="zoneStrJP">日文信息</param>
    public void SetZoneInfo(Sprite zoneImage, string zoneStrCN, string zoneStrJP,bool isLock) {
        this._zoneImage.sprite = zoneImage;
        this._zoneTextCN.text = zoneStrCN;
        this._ZoneTextJP.text = zoneStrJP;
        this.isLock = isLock;
    }
    /// <summary>
    /// 初始化档案
    /// </summary>
    /// <param name="info">档案信息</param>
    public void SetZoneInfo(InstanceZoneInfo info) {
        this._instanceZoneInfo = info;
        Sprite zoneImageSprite = Resources.Load<Sprite>(info.zoneImageURL);
        this._zoneImage.sprite = zoneImageSprite;
        this._zoneTextCN.text = info.zoneTextCN;
        this._ZoneTextJP.text = info.zoneTextJP;
        this.isLock = info.isLock;
    }
    
    
    /// <summary>
    /// 鼠标监听，当鼠标进入到档案时，如果档案没有锁住就显示 _drawGameObject
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        //如果档案锁住，那什么也不做
        if (isLock)return;
        //档案没被锁住，就显示DrawUI
        _drawGameObject.SetActive(true);
        //显示对应播放的video
        VidoPlayController.instance.PlayVideo(_instanceZoneInfo.zoneVideoURL);
    }
    /// <summary>
    /// 鼠标监听，当鼠标离开档案时，如果档案没有锁住就不显示 _drawGameObject
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        //如果档案锁住，那什么也不做
        if (isLock)return;
        //档案没被锁住，就不显示显示DrawUI
        _drawGameObject.SetActive(false);
        //关闭播放的video
        VidoPlayController.instance.PauseCurrentVideoPlay();
    }
    /// <summary>
    /// 鼠标监听,当档案被点击时，如果没有被锁住，就进入对应的编辑页面
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        //TODO:
        //如果档案锁住，那什么也不做
        if (isLock)return;
        Debug.Log("点击了"+_zoneTextCN.text);
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("instanceZoneInfo",_instanceZoneInfo);
        SceneDataManager.GetInstance().ToNewScene("DrawScene",data);
    }
}
