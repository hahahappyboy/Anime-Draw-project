using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake() {
        FetchComponent();
        GetResources();
    }
    protected virtual void Start() {
        InitZoneLayout();
    }
    //获取组件
    protected abstract void FetchComponent();
    //获取数据
    protected abstract void GetResources();
    //设置布局
    protected abstract void InitZoneLayout();
}
