using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconUIVisibility : MonoBehaviour
{
    [SerializeField] private int targetPlayerLayer;
    private List<GameObject> imgs = new List<GameObject>();

    private void Start()
    {
        if (GameManager.Instance.joinPlayers <= targetPlayerLayer) this.gameObject.SetActive(false);

        if(GameManager.Instance.selectStageNo == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                imgs.Add(transform.GetChild(i).gameObject);
            }

            for (int i = 4; i < 8; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 4; i < 8; i++)
            {
                imgs.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    private void Update()
    {
        if(PlayerManager.Instance.playerDatas[targetPlayerLayer].isDeath == true)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if(PlayerManager.Instance.playerDatas[targetPlayerLayer].buffInfo.remainGetItemCT > 0)
        {
            foreach(GameObject icon in imgs.ToArray())
            {
                icon.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject icon in imgs.ToArray())
            {
                icon.SetActive(true);
                icon.transform.LookAt(PlayerManager.Instance.playerDatas[targetPlayerLayer].myBodyTransform);
                icon.transform.localEulerAngles = Vector3.Scale(icon.transform.localEulerAngles, new Vector3(0, 1, 0));
            }
        }
    }
}
