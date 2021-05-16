using UnityEngine;
using UnityEngine.UI;

public class HpUiDisplay : MonoBehaviour
{
    private PlayerManager p;
    private int txtLen;
    [SerializeField] private Text[] hpTxts;

    private void Start()
    {
        p = PlayerManager.Instance;
        txtLen = hpTxts.Length;
    }

    private void Update()
    {
        if (Time.frameCount % 5 != 0) return;
        for(int i = 0; i < txtLen; i++)
        {
            int hp = p.charaDatas[i].hp;
            if (hp <= 0) hpTxts[i].text = "Ž€(s“®•s‰Â)";
            else hpTxts[i].text = hp.ToString();
        }
    }
}
