using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryUiManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private GameEventScript gameEventScript;
    private GameManager gameManager;

    // Texts
    [SerializeField] private Text[] hpTxts;
    [SerializeField] private Text[] isInBedTxts;
    [SerializeField] private Text remainEventStopTxt;
    [SerializeField] private Text remainEventActiveTxt;
    [SerializeField] private Text isPauseTxt;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        gameEventScript = GameEventScript.Instance;
        gameManager = GameManager.Instance;
        //StartCoroutine(FrameWait());
    }

    private void Update()
    {
        // ���׌y��&�e�X�g�p�R�[�h�Ȃ̂�5�t���[����1�x�����X�V
        if (Time.frameCount % 5 != 0) return;

        // HPTxt�X�V
        for (int i = 0; i < hpTxts.Length; i++)
        {
            int hp = playerManager.playerDatas[i].HP;
            if (hp <= 0) hpTxts[i].text = "��(�s���s��)";
            else hpTxts[i].text = hp.ToString();
        }

        // In ���z�c Txt�X�V
        for (int i = 0; i < isInBedTxts.Length; i++)
        {
            if (playerManager.playerDatas[i].isInBed == true) isInBedTxts[i].enabled = true;
            else isInBedTxts[i].enabled = false;
        }

        // �C�x���gTxt�X�V
        remainEventStopTxt.text = $"���C�x���g�J�n����: <size=70>{gameEventScript.remainEventStopTime:000.0}</size>�b";
        remainEventActiveTxt.text = $"�c��C�x���g��������: <size=70>{gameEventScript.remainEventActiveTime:000.0}</size>�b";

        // �|�[�Y��Txt�X�V
        if (gameManager.isPause == true) isPauseTxt.enabled = true;
        else isPauseTxt.enabled = false;

        // �������R��Update�ŏ������Ă��邪�A���t���[��text�X�V�͕��ׂ��|����̂�
        // ���t���[���ĂԕK�v�̂Ȃ����̂͌Ă΂Ȃ�
        // ��) �|�[�Y��UI��enabled��ύX����ۂɖ��t���[���Ď�����K�v�͂Ȃ��A�|�[�Y�{�^���������ꂽ���̂ݕω�������΂���
    }

    IEnumerator FrameWait()
    {
        yield return null;

        playerManager = PlayerManager.Instance;
        gameEventScript = GameEventScript.Instance;
        gameManager = GameManager.Instance;
    }
}
