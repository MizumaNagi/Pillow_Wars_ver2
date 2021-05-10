using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    protected bool isDontDestroy = true;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log(typeof(T).ToString() + "がありません");
            }
            return instance;
        }
    }


    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = GetComponent<T>();
        transform.parent = null;
        if (isDontDestroy == false) { return; }
        StartCoroutine(nameof(StartDontDestroy));
    }

    private IEnumerator StartDontDestroy()
    {
        yield return null;
        DontDestroyOnLoad(gameObject);
    }
}