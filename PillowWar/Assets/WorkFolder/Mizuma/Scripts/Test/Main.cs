using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : SingletonMonoBehaviour<Main>
{
    public void Update()
    {
        Debug.Log(Data.Instance.hoges[5].num);
    }
}