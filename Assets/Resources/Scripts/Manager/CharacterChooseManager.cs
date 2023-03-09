using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using Interface;
using UnityEngine;
using Utils;

public class CharacterChooseManager : BaseMonoBehaviour,INotifier {
    #region 属性
    //角色选择预制体
    private GameObject characterChoosePrefab;
    //characterInfo信息从json获得的
    private List<CharacterChooseInfo> characterInfoList;
    //观察者信息
    private List<IObserver> observerList;
    //单例
    public static CharacterChooseManager instance;
    //目前选中的角色ID
    private int cuurentChooseCharacterID;
    #endregion

    protected override void Awake() {
        base.Awake();
        instance = this;
    }

    protected override void FetchComponent() {
        
    }

    protected override void GetResources() {
        observerList = new List<IObserver>();
        characterChoosePrefab = Resources.Load<GameObject>(Config.CHARACTER_CHOOSE_ITEM_PREFAB_PATH);
        //获取角色信息
        TextAsset characterInfoTextAsset = Resources.Load<TextAsset>(Config.CHARACTERCHOOSEINFO_FIVE_JSON_PATH);
        characterInfoList =  JsonUtils.Json2Class<CharacterChooseInfoItems>(characterInfoTextAsset.text).characterChooseInfoList;
    }
    

    protected override void InitZoneLayout() {
        for (int i = 0; i < characterInfoList.Count; i++) {
            GameObject characterChooseGameObject = Instantiate(characterChoosePrefab);
            characterChooseGameObject.transform.SetParent(this.transform);
            CharacterChooseController controller = characterChooseGameObject.GetComponent<CharacterChooseController>();
            controller.SetCharacterInfo(characterInfoList[i]);
            AddObserver(controller);
            
        }
     
        //默认刚进来就是点击第0个
        if (characterInfoList.Count != 0) {
            CharacterChooseController controller = null;
            //找到第一个CharacterChooseController，并设为默认点击
            for (int i = 0; i < observerList.Count; i++) {
                if (observerList[i] is CharacterChooseController) {
                    controller = observerList[i] as CharacterChooseController;
                    break;
                }
            }
            Dictionary<string, object> message = new Dictionary<string, object>();
            message.Add("characterID",controller.characterChooseInfo.characterID);
            message.Add("isClick",true);
            NotifyObserver(message);
        }
    }
    # region 观察者模式
    public void AddObserver(IObserver observer) {
        observerList.Add(observer);
    }

    public void RemoveObserver(IObserver observer) {
        observerList.Remove(observer);
    }

    public void NotifyObserver(Dictionary<string, object> message) {
        int characterID = message["characterID"] is int ? (int)message["characterID"] : 0 ;
        bool isClick = message["isClick"] is bool ? (bool)message["isClick"] : false;
        //判断是否为点击了的消息，还是说只是鼠标进入的消息
        if (isClick) {
            cuurentChooseCharacterID = characterID;
        }
        foreach (var observer in observerList) {
            observer.Notify(message);
        }
    }
    # endregion
}
