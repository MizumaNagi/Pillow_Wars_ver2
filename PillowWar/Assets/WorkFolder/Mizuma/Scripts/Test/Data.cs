using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : SingletonMonoBehaviour<Data>
{
    public List<Hoge> hoges = new List<Hoge>();

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            hoges.Add(new Hoge(i));
        }
    }
}

public class Hoge
{
    public Hoge(int num)
    {
        this.num = num;
    }

    public int num;
}