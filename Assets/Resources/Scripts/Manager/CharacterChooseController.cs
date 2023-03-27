using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Info;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterChooseController : BaseMonoBehaviour,IObserver,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler {
    // 260 125 ->280 160
    #region 组件
    //背景图片
    private Image bgImage;
    private Image characterImage;
    private AudioSource audioSource;
    #endregion

    #region 属性
    //bgImage的原始material颜色
    private Color bgImageOriginalColor;
    //角色被选中后Image背景的变换颜色
    private Color bgImageChangeColor = new Color(0f,0f,1f,155f/255f);
    //bgImage默认的宽高
    private Vector2 bgImageOriginalHegihtWidth;
    //bgImage点击后的宽高
    private Vector2 bgImageChangeHegihtWidth = new Vector2(280f,160f);
    //dotwoon改变的时间
    private const float DOTWEEN_DURATION = 0.8f;
    //当前角色是否被选中
    private bool isChoosed;
    
    public CharacterChooseInfo characterChooseInfo;

    #endregion
    
    protected override void FetchComponent() {
        audioSource = GetComponent<AudioSource>();
        bgImage = GetComponent<Image>();
        Color bgColor = bgImage.material.color;
        bgColor.a = 155f / 255f;
        bgImageOriginalColor = bgColor;
        bgImageOriginalHegihtWidth = bgImage.rectTransform.sizeDelta;
        characterImage = transform.Find("CharacterMask/CharacterImage").GetComponent<Image>();
    }

    protected override void GetResources() {
    }

    protected override void InitZoneLayout() {
        
    }

    public void SetCharacterInfo(CharacterChooseInfo info) {
        characterChooseInfo = info;
        Sprite characterSprite = Resources.Load<Sprite>(characterChooseInfo.characterImageURL);
        characterImage.sprite = characterSprite;
    }
    /// <summary>
    /// 首先判断是点击事件还是鼠标进入和退出事件
    /// 如果是点击事件：被点击那个改变颜色+变大，其他变小
    /// 如果是进入事件：进入那个改变颜色+变大，其他变小
    /// 如果是退出时间：被点击那个改变颜色+变大，其他变小
    /// </summary>
    /// <param name="message"></param>
    public void Notify(Dictionary<string, object> message) {
        int characterID = message["characterID"] is int ? (int)message["characterID"] : 0;
        bool isClick = message["isClick"] is bool ? (bool)message["isClick"] : false;
        if (isClick) {//如果是点击消息
            //如果选中这个角色就背景变色
            if (characterID == characterChooseInfo.characterID) {
                bgImage.color = bgImageChangeColor;
                bgImage.rectTransform.DOSizeDelta(this.bgImageChangeHegihtWidth,DOTWEEN_DURATION) ;
                isChoosed = true;
            } else {
                bgImage.color = bgImageOriginalColor;
                bgImage.rectTransform.DOSizeDelta(this.bgImageOriginalHegihtWidth,DOTWEEN_DURATION) ;
                isChoosed = false;
            }
        } else {
            //只是鼠标进入消息或则退出消息
            bool isPointerEnter = message["isPointerEnter"] is bool ? (bool)message["isPointerEnter"] : false;
            if (isPointerEnter) {
                if (characterID == characterChooseInfo.characterID) {
                    bgImage.rectTransform.DOSizeDelta(this.bgImageChangeHegihtWidth,DOTWEEN_DURATION) ;
                } else {
                    bgImage.rectTransform.DOSizeDelta(this.bgImageOriginalHegihtWidth,DOTWEEN_DURATION) ;
                }
            } else {
                if (isChoosed) {
                    bgImage.rectTransform.DOSizeDelta(this.bgImageChangeHegihtWidth,DOTWEEN_DURATION) ;
                } else {
                    bgImage.rectTransform.DOSizeDelta(this.bgImageOriginalHegihtWidth,DOTWEEN_DURATION) ;
                }
            }
            
        }
        
    }
    /// <summary>
    /// 点击监听事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("characterID",characterChooseInfo.characterID);
        message.Add("isClick",true);
        CharacterChooseManager.instance.NotifyObserver(message);
        audioSource.clip = MusicManager.instance.GetAudioClip(MusicType.ButtonClick3);
        audioSource.Play();
    }
    public void OnPointerEnter(PointerEventData eventData) {
        // bgImage.rectTransform.DOSizeDelta(this.bgImageChangeHegihtWidth,DOTWEEN_DURATION) ;
        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("characterID",characterChooseInfo.characterID);
        message.Add("isClick",false);
        message.Add("isPointerEnter",true);
        CharacterChooseManager.instance.NotifyObserver(message);
    }
    public void OnPointerExit(PointerEventData eventData) {
        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("characterID",characterChooseInfo.characterID);
        message.Add("isClick",false);
        message.Add("isPointerEnter",false);
        CharacterChooseManager.instance.NotifyObserver(message);
    }
}
