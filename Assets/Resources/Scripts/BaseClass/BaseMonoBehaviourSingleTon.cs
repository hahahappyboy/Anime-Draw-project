using System;
using UnityEngine;
/// <summary>
/// MonoBehaviour单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseMonoBehaviourSingleTon<T> : MonoBehaviour where T: class {
    private static T instance;
        
    public static T Instance {
        get { return instance; }
    }
    
    protected virtual void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this as T; 
            DontDestroyOnLoad(gameObject);
        }
    }

}