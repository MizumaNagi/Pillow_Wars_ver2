using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class BuffTextUpdate : MonoBehaviour
{
    [SerializeField] private int characterNum;

    private CharacterData characterData;
    private Text buffText;
    private RectTransform rect;

    private Vector3 defaultPosP2 = new Vector3(-713, 160, 0);
    private Vector3[] defaultPosP4 = {
        new Vector3(266, -104, 0),
        new Vector3(-313, -104, 0),
        new Vector3(230, 436, 0),
        new Vector3(-313, 436, 0),
    };

    private Vector3[] laterTimePosP2 = {
        new Vector3(0, -400, 0),
        new Vector3(0, 130, 0)
    };
    private Vector3[] laterTimePosP4 ={
        new Vector3(480, -485, 0),
        new Vector3(-480, -485, 0),
        new Vector3(480, 55, 0),
        new Vector3(-480, 55, 0),
    };

    private void Start()
    {
        characterData = PlayerManager.Instance.playerDatas[characterNum];
        buffText = GetComponentInChildren<Text>();
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (characterData.buffInfo.remainBigPillowTime > 0)
        {
            buffText.text = $"枕サイズ上昇 {characterData.buffInfo.remainBigPillowTime.ToString("0")}秒";
        }
        else if (characterData.buffInfo.remainDoubleDmgCount > 0)
        {
            buffText.text = $"ダメージ上昇 {characterData.buffInfo.remainDoubleDmgCount.ToString("0")}回";
        }
        else if (characterData.buffInfo.remainFastSpdTime > 0)
        {
            buffText.text = $"移動速度上昇 {characterData.buffInfo.remainFastSpdTime.ToString("0")}秒";
        }
        else if (characterData.buffInfo.remainFastThrowTime > 0)
        {
            buffText.text = $"投擲速度上昇 {characterData.buffInfo.remainFastThrowTime.ToString("0")}秒";
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator DelayMoveUI()
    {
        yield return new WaitForSeconds(2f);
        if (GameManager.Instance.joinPlayers == 2)
        {
            if (characterNum == 0) rect.localPosition = defaultPosP2 + new Vector3(0, -267f, 0);
            else rect.localPosition = defaultPosP2;
        }
        else
        {
            rect.localPosition = defaultPosP4[characterNum];
        }
    }

    private void OnEnable()
    {
        if (rect == null) return;

        if (GameManager.Instance.joinPlayers == 2)
        {
            if (characterNum == 0) rect.localPosition = laterTimePosP2[0];  // + new Vector3(0, -267f, 0);
            else rect.localPosition = laterTimePosP2[1];
        }
        else
        {
            rect.localPosition = laterTimePosP4[characterNum];
        }

        StartCoroutine(DelayMoveUI());
    }
}
