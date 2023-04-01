using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class InstanceZoneMangerView : MonoBehaviour
{
    # region 组件
    //档案的父物体
    private Transform zonesParent;
    //档案向右按的按钮
    private Button zoneRightMoveButton;
    //档案向左移动的按钮
    private Button zoneLeftMoveButton;
    # endregion


    public void FetchView() {
        zonesParent = this.transform.Find("Viewport/Content").transform;
        //设置点击播放的音乐
        zoneLeftMoveButton = this.transform.Find("Instance Zones Right Button").GetComponent<Button>();
        zoneLeftMoveButton.GetComponent<AudioSource>().clip =
            MusicManager.instance.GetAudioClip(MusicType.ButtonClick1);
        //设置点击播放的音乐
        zoneRightMoveButton = this.transform.Find("Instance Zones Left Button").GetComponent<Button>();
        zoneRightMoveButton.GetComponent<AudioSource>().clip =
            MusicManager.instance.GetAudioClip(MusicType.ButtonClick1);
    }

    public GameObject CreateInstanceZoneGameObject(InstanceZoneInfo instanceZoneInfo) {
        Object zoneLayoutPrefab = Resources.Load<Object>(Config.ZONE_LAYOUT_PREFAB_PATH);
        GameObject zoneLayoutGO = Instantiate(zoneLayoutPrefab, zonesParent) as GameObject;
        zoneLayoutGO.name = instanceZoneInfo.zoneTextCN;
        zoneLayoutGO.transform.localScale = Vector3.one;
        InstanceZoneController zoneController = zoneLayoutGO.GetComponent<InstanceZoneController>();
        zoneController.SetZoneInfo(instanceZoneInfo);
        return zoneLayoutGO;
    }


    public Button GetZoneRightMoveButton() {
        return zoneRightMoveButton;
    }
    public Button GetZoneLeftMoveButton() {
        return zoneLeftMoveButton;
    }

  
}
