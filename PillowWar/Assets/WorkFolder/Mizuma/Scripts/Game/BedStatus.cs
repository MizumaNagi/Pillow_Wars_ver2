using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    [SerializeField] private GameObject emptyBed;
    [SerializeField] private GameObject fullBed;
    [SerializeField] public BoxCollider myEventCollider;

    private bool canInteract;

    public float remainDamageTime;
    public CharacterData data;
    public bool canIn = true;

    private void Start()
    {
        ResetTime();
    }

    private void Update()
    {
        // ベッドが使用状態な場場合何もしない
        if (emptyBed.activeSelf == true || canInteract == false) return;

        // 移動可能かつ非ポーズ状態で睡眠ダメージのリキャスト減少
        if (GameEventScript.Instance.canAction == true && GameManager.Instance.isPause == false) remainDamageTime -= Time.deltaTime;
        if (remainDamageTime < 0) isTimeOver();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isOut">布団から出る状態か</param>
    /// <param name="data">布団に干渉するプレイヤーデータ</param>
    public void ChangeBedActive(bool isOut, CharacterData data)
    {
        emptyBed.SetActive(isOut);
        fullBed.SetActive(!isOut);

        this.data = data;

        canIn = isOut;

        if (isOut == true)
        {
            ResetTime();
        }
    }

    /// <summary>
    /// 時間経過による睡眠ダメージ
    /// </summary>
    private void isTimeOver()
    {
        ResetTime();

        if (data.isInBed == false) return;
        data.Damage(true, false);
    }

    private void ResetTime()
    {
        remainDamageTime = GameManager.Instance.ruleData.inBedDamageTime;
    }

    /// <summary>
    /// 各ステータスのリセット
    /// </summary>
    /// <param name="canInteract"></param>
    public void InitSet(bool canInteract)
    {
        this.canInteract = canInteract;
        emptyBed.SetActive(canInteract);
        fullBed.SetActive(!canInteract);
        myEventCollider.enabled = canInteract;
    }
}
