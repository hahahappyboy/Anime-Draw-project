using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CustomStructure;
using DG.Tweening;
using Info;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Object = UnityEngine.Object;
 /// <summary>
 /// 整个档案管理的
 /// </summary>
public class InstanceZoneManager : BaseMonoBehaviour{
    # region 组件
    //档案的父物体
    private Transform zonesParent;
    //档案向右按的按钮
    private Button zoneRightMoveButton;
    //档案向左移动的按钮
    private Button zoneLeftMoveButton;
    # endregion
    
    # region 属性
    //档案的有关信息
    [Header("档案列表")] 
    [SerializeField] 
    private List<InstanceZoneInfo> instanceZoneInfoList;
    //档案链表
    private CircularDoubleLinkedList<InstanceZoneController> _instanceZoneControllersCircularDoubleLinkedList;
    //档案移动的时间
    [Header("档案列表")] 
    public const float ZONE_MOVE_DURATION = 0.5f;
    //档案每次滑动的个数
    public const int ZONE_MOVE_NUM = 4;
    //档案是否正在移动
    private bool zoneIsMoving = false;
    # endregion
    
    
    protected override void Awake() {
       base.Awake();
    }

    protected override void Start() {
        base.Start();
    }
    
    /// <summary>
    /// 获取本地档案信息
    /// </summary>
    protected override void GetResources() {
        //获取本地档案Json
        TextAsset zoneTextAsset = Resources.Load<TextAsset>(Config.INSTANCEZONEINFO_JSON_PATH);
        //json转化成list
        instanceZoneInfoList = JsonUtils.Json2Class<InstanceZoneInfoItems>(zoneTextAsset.text).instanceZoneInfoList;
    }

    /// <summary>
    /// 初始化所有档案
    /// </summary>
    protected override void InitZoneLayout() {
        if (instanceZoneInfoList==null) Debug.LogWarning("档案初始化失败，没有获得信息");
        _instanceZoneControllersCircularDoubleLinkedList = new CircularDoubleLinkedList<InstanceZoneController>();
        //加载档案
        Object zoneLayoutPrefab = Resources.Load<Object>(Config.ZONE_LAYOUT_PREFAB_PATH);
        for (int i = 0; i < instanceZoneInfoList.Count; i++) {
            GameObject zoneLayoutGO = Instantiate(zoneLayoutPrefab, zonesParent) as GameObject;
            zoneLayoutGO.name = instanceZoneInfoList[i].zoneTextCN;
            zoneLayoutGO.transform.localScale = Vector3.one;
            InstanceZoneController zoneController = zoneLayoutGO.GetComponent<InstanceZoneController>();
            zoneController.SetZoneInfo(instanceZoneInfoList[i]);
            //加入循环链表
            CircularDoubleLinkedListNode<InstanceZoneController> zoneControllerNode =
                new CircularDoubleLinkedListNode<InstanceZoneController>(zoneController);
            _instanceZoneControllersCircularDoubleLinkedList.AddLast(zoneControllerNode);
        }
    }

    /// <summary>
    /// 获取所需要的组件
    /// </summary> 
    protected override void FetchComponent() {
        zonesParent = this.transform.Find("Viewport/Content").transform;
        zoneLeftMoveButton = this.transform.Find("Instance Zones Right Button").GetComponent<Button>();
        zoneLeftMoveButton.onClick.AddListener(ButtonSlideLeft);
        zoneRightMoveButton = this.transform.Find("Instance Zones Left Button").GetComponent<Button>();
        zoneRightMoveButton.onClick.AddListener(ButtonSlideRight);
    }

    # region 按钮监听
    /// <summary>
    /// 向左按钮，记录5个节点位置，实现滑动，中间3个为显示的节点，旁边两个为不显示的节点
    /// </summary>
    public void ButtonSlideLeft() {
        //如果没有3个档案就不滑动
        if (_instanceZoneControllersCircularDoubleLinkedList.Count<=3) return;
        //档案在移动也什么也不作
        if (zoneIsMoving) return;
        zoneIsMoving = true;
        // 要移动的节点
        CircularDoubleLinkedListNode<InstanceZoneController> moveZoneNode;
        moveZoneNode = _instanceZoneControllersCircularDoubleLinkedList.First;
        //本次移动完后的最后一个节点
        CircularDoubleLinkedListNode<InstanceZoneController> moveLastZoneNode = moveZoneNode;
        for (int i = 0; i < ZONE_MOVE_NUM-1; i++) {
            moveLastZoneNode = moveLastZoneNode.Next;
        }
        //最后一个节点的位置
        Vector3 moveLastZonePos = moveLastZoneNode.Value.transform.position;
        //要移动的距离
        Vector3 moveDis = moveZoneNode.Value.transform.position - moveZoneNode.Next.Value.transform.position;
        //移动，从第1个节点开始，往前移动
        for (int i = 1; i <= ZONE_MOVE_NUM; i++) {
            //移动的目标
            Vector3 targetPos = moveZoneNode.Value.transform.position + moveDis;
            var doMove = moveZoneNode.Value.transform.DOMove(targetPos, ZONE_MOVE_DURATION);
            moveZoneNode = moveZoneNode.Next;
            //当最后一个档案移动完毕哦，执行回调，设置下一次移动时，最后一个档案的位置
            if (i==ZONE_MOVE_NUM) {
                doMove.OnComplete(() => {
                    moveLastZoneNode = moveLastZoneNode.Next;
                    moveLastZoneNode.Value.transform.DOMove(moveLastZonePos, ZONE_MOVE_DURATION - ZONE_MOVE_DURATION).OnComplete(
                        () => {
                            //最后一个档案移动完才可再次点击按钮
                            zoneIsMoving = false;
                        });
                        
                });
            }
        }
        //重新设置头节点
        _instanceZoneControllersCircularDoubleLinkedList.First = _instanceZoneControllersCircularDoubleLinkedList.First.Next;
    }
    
    /// <summary>
    /// 按钮向右滑动
    /// </summary>
    public void ButtonSlideRight() {
        //如果没有3个档案就不滑动
        if (_instanceZoneControllersCircularDoubleLinkedList.Count<=3) return;
        //档案在移动也什么也不作
        if (zoneIsMoving) return;
        zoneIsMoving = true;
        // 要移动的节点
        CircularDoubleLinkedListNode<InstanceZoneController> moveZoneNode;
        //从左边被隐藏的那个开始向右移动
        moveZoneNode = _instanceZoneControllersCircularDoubleLinkedList.First.Previous;
        //头结点
        CircularDoubleLinkedListNode<InstanceZoneController> firstNode = _instanceZoneControllersCircularDoubleLinkedList.First ;
        //要移动的距离
        Vector3 moveDis = firstNode.Next.Value.transform.position - firstNode.Value.transform.position;
        //左边被隐藏的那个的位置设置到First的左边，并且重新设置其Position(不重新设置会有问题)
        moveZoneNode.Value.transform.DOMove(_instanceZoneControllersCircularDoubleLinkedList.First.Value.transform.position - moveDis, 0f);
        moveZoneNode.Value.transform.position = firstNode.Value.transform.position - moveDis;
        //移动，从第1个节点开始，往前移动
        for (int i = 1; i <= ZONE_MOVE_NUM; i++) {
            //移动的目标
            Vector3 targetPos = moveZoneNode.Value.transform.position + moveDis;
            var doMove = moveZoneNode.Value.transform.DOMove(targetPos, ZONE_MOVE_DURATION);
            moveZoneNode = moveZoneNode.Next;
            //当最后一个档案移动完毕哦，执行回调，设置下一次移动时，最后一个档案的位置
            if (i==ZONE_MOVE_NUM) {
                doMove.OnComplete(() => {
                    zoneIsMoving = false;
                });
            }
        }
        //重新设置头节点
        _instanceZoneControllersCircularDoubleLinkedList.First = _instanceZoneControllersCircularDoubleLinkedList.First.Previous;
    }
    # endregion
}
