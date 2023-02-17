using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstanceZoneController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {
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
    # endregion

    private void Awake() {
        //获取所需要的组件
        FetchComponent();
    }

    private void Start() {
        //初始化档案界面       
        InitScreen();
    }
    /// <summary>
     /// 初始化档案界面
     /// </summary>
    private void InitScreen() {
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
    private void FetchComponent() {
        _zoneImage = this.transform.Find("Image").GetComponent<Image>();
        _lockGameObject = this.transform.Find("Image/Lock").gameObject;
        _drawGameObject = this.transform.Find("Image/Draw").gameObject;
        _zoneTextCN = this.transform.Find("TextBG/Text CN").GetComponent<Text>();
        _ZoneTextJP = this.transform.Find("TextBG/Text JP").GetComponent<Text>();
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
    /// 鼠标监听，当鼠标进入到档案时，如果档案没有锁住就显示 _drawGameObject
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        //如果档案锁住，那什么也不做
        if (isLock)return;
        //档案没被锁住，就显示 DrawUI
        _drawGameObject.SetActive(true);
    }
    /// <summary>
    /// 鼠标监听，当鼠标离开档案时，如果档案没有锁住就不显示 _drawGameObject
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        //如果档案锁住，那什么也不做
        if (isLock)return;
        //档案没被锁住，就不显示显示 DrawUI
        _drawGameObject.SetActive(false);
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
    }
}
