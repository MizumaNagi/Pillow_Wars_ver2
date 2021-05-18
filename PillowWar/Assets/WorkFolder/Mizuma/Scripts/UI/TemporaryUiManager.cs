using UnityEngine;
using UnityEngine.UI;

public class TemporaryUiManager : MonoBehaviour
{
    private PlayerManager p;
    private GameEventScript g;
    private int txtLen;
    [SerializeField] private Text[] hpTxts;
    [SerializeField] private Text remainEventStopTxt;
    [SerializeField] private Text remainEventActiveTxt;

    private void Start()
    {
        p = PlayerManager.Instance;
        g = GameEventScript.Instance;
        txtLen = hpTxts.Length;
    }

    private void Update()
    {
        if (Time.frameCount % 5 != 0) return;
        for(int i = 0; i < txtLen; i++)
        {
            int hp = p.charaDatas[i].hp;
            if (hp <= 0) hpTxts[i].text = "��(�s���s��)";
            else hpTxts[i].text = hp.ToString();
        }
        remainEventStopTxt.text = $"�c��C�x���g�Î~����: <size=70>{g.remainEventStopTime:000.0}</size>�b";
        remainEventActiveTxt.text = $"�c��C�x���g�p������: <size=70>{g.remainEventActiveTime:000.0}</size>�b";
    }
}
