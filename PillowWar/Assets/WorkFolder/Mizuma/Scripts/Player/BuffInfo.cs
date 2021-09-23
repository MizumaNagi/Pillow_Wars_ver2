using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo : MonoBehaviour
{
    public void GetItem(ItemInfo itemInfo)
    {
        if (GetCanGetItem() == false) return;

        AudioManager.Instance.SEPlay(SEName.GetItem);

        Debug.Log(itemInfo.itemName);

        getItemName = itemInfo.itemName;
        remainFastSpdTime = itemInfo.fastMoveSpdTime;
        remainBigPillowTime = itemInfo.bigPillowTime;
        remainDoubleDmgCount = itemInfo.doubleDmgCnt;
        remainFastThrowTime = itemInfo.upThrowSpdTime;
        remainGetItemCT = GameManager.Instance.itemData.canGetItemCT;
    }

    private bool GetCanGetItem()
    {
        if (remainDoubleDmgCount <= 0 && remainGetItemCT < 0) return true;
        else return false;
    }

    public ITEM_NAME getItemName;
    public float remainFastSpdTime;
    public float remainBigPillowTime;
    public int remainDoubleDmgCount;
    public float remainFastThrowTime;

    public float remainGetItemCT;

    private void Update()
    {
        if (GameManager.Instance.isPause == false && GameEventScript.Instance.canAction == true)
        {
            float deltaTime = Time.deltaTime;
            remainFastSpdTime -= deltaTime;
            remainBigPillowTime -= deltaTime;
            remainFastThrowTime -= deltaTime;
            remainGetItemCT -= deltaTime;
        }
    }
}
