using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;

    public static T instance
    {
        get {
            return _instance = FindOrCreateIfEmpty();
        }
    }

    protected virtual void Awake()
    {
        if(_instance != null) Destroy(gameObject);
        _instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    private static T FindOrCreateIfEmpty()
    {
        T instance = null;
        if (instance == null) instance = FindObjectOfType<T>();
        if (instance == null)
        {
            instance = CreateInstanceWithTypeNameAndComponent();
        }
        return instance;
    }

    private static T CreateInstanceWithTypeNameAndComponent()
    {
        return new GameObject(typeof(T).Name).AddComponent<T>();
    }
}
