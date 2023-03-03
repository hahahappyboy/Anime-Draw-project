using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : BaseMonoBehaviour
{
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    protected override void FetchComponent() {
        
    }

    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }
}
