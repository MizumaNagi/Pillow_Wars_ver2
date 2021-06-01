using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour
{
    // �e�v���C���[��HPIcon��Z�߂�eObjects
    [SerializeField] private Transform[] hpIconParents;

    // HP�A�C�R���̐�(�S�v���C���[�������ȑO��)
    private int iconChildCount = 0;

    // �Q���l��(��ʕ�����)
    private int joinPlayers = 0;

    // �S�v���C���[�̑S�A�C�R��Image���i�[����
    // List<Image>�^��List ��������List�Ƃ�...?�ƂȂ����� "C# List"�Ō���
    // hpIcons[0][3] ������Ȋ����Ŏ擾(1(0)�Ԗڂ̃v���C���[��4(3)�Ԗڂ�HpIcon���擾)
    private List<List<Image>> hpIcons = new List<List<Image>>();

    private void Start()
    {
        iconChildCount = hpIconParents[0].childCount;
        joinPlayers = GameManager.Instance.joinPlayers;

        SetHpIcons();
    }

    private void Update()
    {
        // �|�[�Y����HPUI�̍X�V���s��Ȃ�
        if (GameManager.Instance.isPause == true) return;

        for(int i = 0; i < joinPlayers; i++)
        {
            // i �Ԗڂ�Player�̌���HP�������Z�o����
            float hpPercentage = (float)PlayerManager.Instance.charaDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

            // HP���������Ƃ�Icon�����
            for(int j = 0; j < iconChildCount; j++)
            {
                hpIcons[i][j].fillAmount = hpPercentage * iconChildCount - 1 * j;
            }
        }
    }

    // HpIcon���擾���Ċi�[����
    private void SetHpIcons()
    {
        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            // hpIcons��Add����V�KList<Image>��p�ӂ���
            List<Image> imgs = new List<Image>();

            // �A�C�R���������[�v����GetComponent��Image��ϐ��ɓ���Ă���
            for(int j = 0; j < iconChildCount; j++)
            {
                // ���ɏd�����Ȃ�\���L�A�v���P
                imgs.Add(hpIconParents[i].GetChild(j).GetComponent<Image>());
            }
            // �������V�K�ō����List��Image�����I������̂ł���List��hpIcons��Add����
            hpIcons.Add(imgs);
        }
    }
}
