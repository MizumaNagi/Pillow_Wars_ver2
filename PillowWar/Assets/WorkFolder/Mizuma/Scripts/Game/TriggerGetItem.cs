

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TriggerGetItem : MonoBehaviour
{
    private int itemIndex;

    private void Start()
    {
        itemIndex = (int)ITEM_NAME.LENGTH;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StringBuilder charaName = new StringBuilder(other.gameObject.name);
            charaName.Replace("Player", "");
            charaName.Replace("Npc", "");

            int charaIndex = int.Parse(charaName.ToString());

            CharacterData data;
            if (charaIndex >= 100) data = PlayerManager.Instance.npcDatas[charaIndex - 100];
            else data = PlayerManager.Instance.playerDatas[charaIndex];

            int rndIndex = Random.Range(0, itemIndex);
            data.buffInfo.GetItem(GameManager.Instance.itemData.itemInfos[rndIndex]);
        }
    }
}
