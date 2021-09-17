using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemInfo[] itemInfos;
    public float fastMoveSpdMulti;
    public float bigPillowScale;
    public float upThrowMulti;
    public float canGetItemCT;
}

public enum ITEM_NAME
{
    MoveFast,
    BigPillow,
    DoubleDmg,
    ThrowFast,
    LENGTH
}

[System.Serializable]
public class ItemInfo
{
    public ITEM_NAME itemName;

    public float fastMoveSpdTime;

    public float bigPillowTime;

    public int doubleDmgCnt;

    public float upThrowSpdTime;
}
