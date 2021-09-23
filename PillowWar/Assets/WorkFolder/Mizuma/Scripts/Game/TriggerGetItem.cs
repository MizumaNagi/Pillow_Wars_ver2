using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TriggerGetItem : MonoBehaviour
{
    [SerializeField] private GameObject[] buffUIsP2;
    [SerializeField] private GameObject[] buffUIsP4;

    private int itemIndex;

    private void Start()
    {
        itemIndex = (int)ITEM_NAME.LENGTH;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // ID�`�F�b�NS
            StringBuilder charaName = new StringBuilder(other.gameObject.name);
            charaName.Replace("Player", "");
            charaName.Replace("Npc", "");

            int charaIndex = int.Parse(charaName.ToString());

            // �Ώۂ�Data�擾
            CharacterData data;
            if (charaIndex >= 100) data = PlayerManager.Instance.npcDatas[charaIndex - 100];
            else data = PlayerManager.Instance.playerDatas[charaIndex];

            // �o�t�K�p
            int rndIndex = Random.Range(0, itemIndex);
            data.buffInfo.GetItem(GameManager.Instance.itemData.itemInfos[rndIndex]);

            // UI�\��
            if (charaIndex >= 100) return;

            if(GameManager.Instance.joinPlayers == 2)
            {
                buffUIsP2[charaIndex].SetActive(true);
            }
            else
            {
                buffUIsP4[charaIndex].SetActive(true);
            }
        }
    }
}
