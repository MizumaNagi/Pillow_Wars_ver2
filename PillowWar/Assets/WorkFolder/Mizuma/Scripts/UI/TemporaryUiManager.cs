using UnityEngine;
using UnityEngine.UI;

public class TemporaryUiManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private GameEventScript gameEventScript;
    private GameManager gameManager;

    // Texts
    [SerializeField] private Text[] hpTxts;
    [SerializeField] private Text remainEventStopTxt;
    [SerializeField] private Text remainEventActiveTxt;
    [SerializeField] private Text isPauseTxt;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        gameEventScript = GameEventScript.Instance;
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        // 負荷軽減&テスト用コードなので5フレームに1度だけ更新
        if (Time.frameCount % 5 != 0) return;

        // HPTxt更新
        for(int i = 0; i < hpTxts.Length; i++)
        {
            int hp = playerManager.charaDatas[i].hp;
            if (hp <= 0) hpTxts[i].text = "死(行動不可)";
            else hpTxts[i].text = hp.ToString();
        }

        // イベントTxt更新
        remainEventStopTxt.text = $"残りイベント静止時間: <size=70>{gameEventScript.remainEventStopTime:000.0}</size>秒";
        remainEventActiveTxt.text = $"残りイベント継続時間: <size=70>{gameEventScript.remainEventActiveTime:000.0}</size>秒";

        // ポーズ中Txt更新
        if (gameManager.isPause == true) isPauseTxt.enabled = true;
        else isPauseTxt.enabled = false;

        // さも当然にUpdateで処理しているが、毎フレームtext更新は負荷が掛かるので
        // 毎フレーム呼ぶ必要のないものは呼ばない
        // 例) ポーズ中UIのenabledを変更する際に毎フレーム監視する必要はなく、ポーズボタンが押された時のみ変化させればいい
    }
}
