using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour
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
        if (instance != null)
        {
            Debug.Log("重複しているので削除しました。\n" + "type: " + typeof(T).ToString());
            Destroy(gameObject);
            return;
        }

        instance = GetComponent<T>();
        if (instance == null)
        {
            Debug.Log("取得に失敗しました。");
        }
        transform.parent = null;

        if (isDontDestroy == false) { return; }
        DontDestroyOnLoad(gameObject);

    }
}