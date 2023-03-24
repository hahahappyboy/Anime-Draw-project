using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : BaseMonoBehaviour
{
    //用于判断保证音乐只创建一次
    private static bool origional = true;
    protected override void Awake() {
        base.Awake();
        if (origional) {
            origional = false;
            DontDestroyOnLoad(this.gameObject);
        }else {
            //如果不是最创建的那个就直接销毁
            Destroy(this.gameObject);
        }

        
    }

    protected override void FetchComponent() {
        
    }

    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }
}
