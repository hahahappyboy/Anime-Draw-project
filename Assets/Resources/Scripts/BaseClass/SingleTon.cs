
using System;
using UnityEngine;
/// <summary>
/// 单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingleTon<T> where T: class {
    private static T instance;

    public static T GetInstance() {
        if (instance==null) {
            instance = Activator.CreateInstance(typeof(T), true) as T;
        }

        return instance;
    }

    protected BaseSingleTon() {
        
    }
}
