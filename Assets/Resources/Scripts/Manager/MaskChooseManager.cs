using System.Collections;
using System.Collections.Generic;
using Frame;
using Info;
using Interface;
using UnityEngine;
using Utils;

public class MaskChooseManager : BaseMonoBehaviour,IObserver
{
    #region 属性
    //string用来存id，list用来存信息。例如3表示这是三玖的所有maskinfo
    private Dictionary<int, List<MaskChooseInfo>> maskChooseItemDic;
    //目前选择的角色ID
    private int curentChooseCharacterID = -1;
    //创建的prefab的名字
    private const string PREFAB_NAME = "MaskChooseItem";
    //存放目前展示的maskGameObject
    private List<MaskChooseController> currentShowMaskController;
    //item的父物体
    private Transform prefabFather;
    #endregion

    protected override void Start() {
        base.Start();
        //注册为CharacterChooseManager的观察者
        CharacterChooseManager.instance.AddObserver(this);
    }

    protected override void FetchComponent() {
        prefabFather = transform.Find("Scroll View/Viewport/Content").transform;
    }

    protected override void GetResources() {
        maskChooseItemDic = new Dictionary<int, List<MaskChooseInfo>>();
        currentShowMaskController = new List<MaskChooseController>();
        
        TextAsset maskInfoTextAsset = Resources.Load<TextAsset>(SceneDataManager.GetInstance().GetMaskJsonPath());
        List<MaskChooseInfo> maskInfoList =  JsonUtils.Json2Class<MaskChooseInfoItems>(maskInfoTextAsset.text).maskChooseInfoList;
        //把信息按ID分类
        foreach (var item in maskInfoList) {
            if (!maskChooseItemDic.ContainsKey(item.characterID)) {
                List<MaskChooseInfo> info = new List<MaskChooseInfo>();
                info.Add(item);
                maskChooseItemDic.Add(item.characterID,info);
            } else {
                maskChooseItemDic[item.characterID].Add(item);
            }
        }
    }
    
    protected override void InitZoneLayout() {
        
    }

    /// <summary>
    /// 但选择角色改变时，更改mask
    /// </summary>
    /// <param name="message"></param>
    public void Notify(Dictionary<string, object> message) {
        bool isClick = message["isClick"] is bool ? (bool)message["isClick"] : false;
        if (!isClick) return;
        int characterID = message["characterID"] is int ? (int)message["characterID"] : 0 ;
        //选中的还是之前的ID，所以不相应
        if (curentChooseCharacterID == characterID) {
            return;
        }
        curentChooseCharacterID = characterID;
        if (!maskChooseItemDic.ContainsKey(characterID)) {
            Debug.LogError("没有对应角色的mask");
            return;
        }
        //回收目前展示的对象池
        for (int i = 0; i < currentShowMaskController.Count; i++) {
            ObjectPool.GetInstance().RecycleGameObject(currentShowMaskController[i].gameObject);
        }
        //调用对象池重新创建
        List<MaskChooseInfo> maskChooseInfoList = maskChooseItemDic[characterID];
        foreach (var maskChooseInfo in maskChooseInfoList) {
            GameObject maskItemGameObject = ObjectPool.GetInstance().CreateGameObject(PREFAB_NAME, Config.MASK_CHOOSE_ITEM_PREFAB_PATH);
            MaskChooseController controller = maskItemGameObject.GetComponent<MaskChooseController>();
            controller.SetMaskChooseInfo(maskChooseInfo);
            controller.transform.SetParent(prefabFather);
            currentShowMaskController.Add(controller);
        }
        //将展示的缩放设置为1
        for (int i = 0; i < currentShowMaskController.Count; i++) {
            currentShowMaskController[i].transform.localScale = Vector3.one;
        }
    }
}
