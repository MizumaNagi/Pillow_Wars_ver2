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

    private Vector3[] defaultPosP2 = {
        new Vector3(-702, -78, 0),
        new Vector3(-702, 194, 0)};

    private Vector3[] defaultPosP4 = {
        new Vector3(258, -78, 0),
        new Vector3(-313, -78, 0),
        new Vector3(258, 465, 0),
        new Vector3(-313, 465, 0),
    };

    private Vector3[] laterTimePosP2 = {
        new Vector3(15, -371, 0),
        new Vector3(15, -101, 0)
    };
    private Vector3[] laterTimePosP4 ={
        new Vector3(492, -396, 0),
        new Vector3(-447, -396, 0),
        new Vector3(492, 147, 0),
        new Vector3(-447, 147, 0),
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
            buffText.text = $"���T�C�Y�㏸ {characterData.buffInfo.remainBigPillowTime.ToString("0")}�b";
        }
        else if (characterData.buffInfo.remainDoubleDmgCount > 0)
        {
            buffText.text = $"�_���[�W�㏸ {characterData.buffInfo.remainDoubleDmgCount.ToString("0")}��";
        }
        else if (characterData.buffInfo.remainFastSpdTime > 0)
        {
            buffText.text = $"�ړ����x�㏸ {characterData.buffInfo.remainFastSpdTime.ToString("0")}�b";
        }
        else if (characterData.buffInfo.remainFastThrowTime > 0)
        {
            buffText.text = $"�������x�㏸ {characterData.buffInfo.remainFastThrowTime.ToString("0")}�b";
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator DelayMoveUI()
    {
        Debug.Log("UI�ړ��J�n");
        yield return new WaitForSeconds(2f);
        Debug.Log("UI�ړ��I��");
        if (GameManager.Instance.joinPlayers == 2)
        {
            rect.localPosition = defaultPosP2[characterNum];
        }
        else
        {
            rect.localPosition = defaultPosP4[characterNum];
        }
    }

    private void OnEnable()
    {
        if (rect == null) return;

        Debug.Log("�A�C�e���擾");

        if (GameManager.Instance.joinPlayers == 2)
        {
            rect.localPosition = laterTimePosP2[characterNum];

            //if (characterNum == 0) rect.localPosition = laterTimePosP2[0];  // + new Vector3(0, -267f, 0);
            //else rect.localPosition = laterTimePosP2[1];
        }
        else
        {
            rect.localPosition = laterTimePosP4[characterNum];
        }

        StartCoroutine(DelayMoveUI());
    }
}
