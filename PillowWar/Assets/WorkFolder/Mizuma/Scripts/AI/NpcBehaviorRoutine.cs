﻿using System.Text;
using UnityEngine;
using UnityEngine.AI;

public enum NPC_STATUS
{
    WALK,
    GO_ENEMY,
    GO_BED,
    IN_BED,
    PILLOW_THROW,
    LENGTH
}

public class NpcBehaviorRoutine : MonoBehaviour
{
    [SerializeField] public NpcRoutineData routineData;
    [SerializeField] public SphereCollider searchCollider;

    public CharacterData characterData;
    public CharacterMover characterMover = new CharacterMover();
    public NPC_STATUS npcStatus = NPC_STATUS.WALK;

    private NavMeshAgent agent;
    private CharacterData targetData;
    private GameObject targetMarkObj;
    public int npcID;
    private float defaultSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNpcID();
        characterData = PlayerManager.Instance.npcDatas[npcID - 100];
        searchCollider.radius = routineData.warRangeToEnemy / 2;

        targetMarkObj = Instantiate(routineData.targetMark);
        GameEventScript.Instance.npcBehaviorRoutines.Add(this);
        defaultSpeed = agent.speed;
    }

    private void Update()
    {
        if (gameObject.activeSelf == false) this.enabled = false;

        if (GameManager.Instance.isPause == false)
        {
            agent.isStopped = false;
            UpdateActivity();
        }
        else
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }

    /// <summary>
    /// NPC名から自身のIDを取得する
    /// </summary>
    private void GetNpcID()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        sb.Replace("Npc", "");
        if (int.TryParse(sb.ToString(), out npcID) == false) Debug.LogError("NpcID取得失敗");
        sb.Clear();
    }

    /// <summary>
    /// 次の移動先をランダムで見つめる(NPC_STATUS.WALK時専用)
    /// </summary>
    /// <returns>次の移動先</returns>
    private Vector3 GetNextDestination()
    {
        NavMeshHit navMeshHit;

        Vector3 rndPos = new Vector3(Random.Range(-routineData.stageRange.x, routineData.stageRange.x), 0, Random.Range(-routineData.stageRange.z, routineData.stageRange.z));
        if (NavMesh.SamplePosition(rndPos, out navMeshHit, routineData.searchNavMeshRange, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
            Debug.LogError("Ai目的地の設定失敗");
            return Vector3.zero;
        }
    }

   /// <summary>
   /// 最も近い未使用ベッドの座標を取得する
   /// </summary>
   /// <returns>最も近い未使用ベッドの座標</returns>
    private Vector3 GetShortestBedPos()
    {
        float shortestBedPos = Mathf.Infinity;
        int shortIndex = -1;

        // BedManagerに登録されている全ての未使用ベッドから最も近いベッドのインデックス番号を取得する
        for (int i = 0; i < BedManager.Instance.bedColliders.Count; i++)
        {
            if (BedManager.Instance.bedColliders[i].enabled == false) continue;

            float distance = Vector3.Distance(transform.position, BedManager.Instance.bedColliders[i].transform.position);
            if (distance < shortestBedPos)
            {
                shortestBedPos = distance;
                shortIndex = i;
            }
        }

        // 最も近いベッドのインデックスが初期値以外だったか？
        if (shortIndex != -1)
        {
            characterData.bedStatus = GetDestinationBedStatus(shortIndex);
            return BedManager.Instance.bedColliders[shortIndex].transform.position;
        }
        else
        {
            Debug.Log("布団見つけられず...");
            SetNpcStatus(NPC_STATUS.WALK);
            return Vector3.zero;
        }
    }

    /// <summary>
    /// BedStatusを取得する
    /// </summary>
    /// <param name="index">布団の配列番号</param>
    /// <returns>指定した入れる番号のBedStatus</returns>
    private BedStatus GetDestinationBedStatus(int index)
    {
        return BedManager.Instance.bedColliders[index].GetComponent<BedStatus>();
    }

    /// <summary>
    /// NPCのステータスを設定する
    /// 変化時に1度だけ行う処理も記載する
    /// </summary>
    /// <param name="status">NPCステータス</param>
    public void SetNpcStatus(NPC_STATUS status)
    {
        npcStatus = status;

        switch (status)
        {
            case NPC_STATUS.WALK:
                {
                    agent.stoppingDistance = 0;
                    break;
                }
            case NPC_STATUS.GO_ENEMY:
                {
                    agent.stoppingDistance = routineData.distanceToEnemy;
                    break;
                }
            case NPC_STATUS.GO_BED:
                {
                    agent.stoppingDistance = 0;
                    TriggerGoBed();
                    break;
                }
            case NPC_STATUS.IN_BED:
                {
                    break;
                }
            case NPC_STATUS.PILLOW_THROW:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    /// <summary>
    /// NPCステータスに合わせて毎フレーム行う処理
    /// </summary>
    private void UpdateActivity()
    {
        switch (npcStatus)
        {
            case NPC_STATUS.WALK:
                {
                    // 経路が無ければ新しい目的地を決める
                    if (agent.hasPath == false)
                    {
                        Vector3 nextPos = GetNextDestination();
                        agent.destination = nextPos;
                        targetMarkObj.transform.position = nextPos;
                        SetNpcStatus(NPC_STATUS.WALK);
                    }
                    break;
                }
            case NPC_STATUS.GO_ENEMY:
            case NPC_STATUS.PILLOW_THROW:
                {
                    // 攻撃対象が死んだら別ターゲットを探しに行く
                    if (targetData.HP <= 0)
                    {
                        ResetTarget(); 
                        break;
                    }

                    if (agent.hasPath == false)
                    {
                        Debug.LogWarning("経路なし");
                        break;
                    }

                    // ターゲットの座標更新
                    agent.destination = targetData.character.transform.position;
                    LookAtTarget();

                    // 接敵距離まで接近したら枕を投げる
                    if (agent.remainingDistance <= routineData.warRangeToEnemy)
                    {
                        if (characterData.remainthrowCT < 0 && characterData.isHavePillow) characterMover.PillowThrow(characterData, true);
                    }

                    break;
                }
            case NPC_STATUS.GO_BED:
                {
                    if (characterData.bedStatus != null)
                    {
                        if (characterData.bedStatus.canIn == false)
                        {
                            Debug.Log("割り込み");
                            SetNpcStatus(NPC_STATUS.WALK);
                            //StandUpBed();
                        }
                    }
                    break;
                }
            case NPC_STATUS.IN_BED:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    /// <summary>
    /// 攻撃対象の方を向く
    /// </summary>
    private void LookAtTarget()
    {
        transform.LookAt(agent.destination);
    }

    /// <summary>
    /// IDに沿ったCharacterDataを取得する
    /// </summary>
    /// <param name="id">取得したい相手のID</param>
    /// <returns>指定したIDのCharacterData</returns>
    private CharacterData GetChatacterData(int id)
    {
        if (id < 100) return PlayerManager.Instance.playerDatas[id];
        else return PlayerManager.Instance.npcDatas[id - 100];
    }

    /// <summary>
    /// ターゲットをリセットして、歩行状態に戻す
    /// </summary>
    private void ResetTarget()
    {
        targetData = null;
        SetNpcStatus(NPC_STATUS.WALK);
    }

    /// <summary>
    /// 襲撃イベントを行うか、無視するか
    /// </summary>
    private void TriggerGoBed()
    {
        // 残りHP割合が襲撃イベ開始HP割合を下回っていなければイベント無視
        float remainHpPercent = ((float)characterData.HP / (float)GameManager.Instance.ruleData.maxHp) * 100;
        if (remainHpPercent >= routineData.startGoBedRemHpPercent)
        {
            SetNpcStatus(NPC_STATUS.WALK);
            return;
        }

        // 残りHP割合を元にイベント実行率を計算、イベント実行率よりもRandom(0,100)の方が多ければイベント無視
        float bedEventThroughPercent = Mathf.Clamp01(remainHpPercent / routineData.startGoBedRemHpPercent) * 100;
        int rnd = Random.Range(0, 100);

        Debug.Log($"※ 襲撃イベント\n失敗値: {bedEventThroughPercent}, " +
            $"成功値: {rnd} + {routineData.minStartGoBedPercent}, " +
            $"結果: {bedEventThroughPercent < rnd + routineData.minStartGoBedPercent}");
        if (bedEventThroughPercent > rnd + routineData.minStartGoBedPercent) 
        {
            SetNpcStatus(NPC_STATUS.WALK);
            return; 
        }

        // 座標を取得、目的地に設定
        Vector3 nextPos = GetShortestBedPos();
        agent.destination = nextPos;
        Debug.Log(characterData.GetID(true));
        targetMarkObj.transform.position = nextPos;
    }

    /// <summary>
    /// ベッドに入退出時の処理
    /// </summary>
    /// <param name="isInBed">ベッドに入ろうとしているか</param>
    public void InteractBed(bool isInBed)
    {
        if (isInBed)
        {
            agent.destination = characterData.inBedPos;
            targetMarkObj.transform.position = characterData.inBedPos;
            agent.speed = 0;
        }
        else
        {
            PlayerManager.Instance.playerDatas[characterData.GetID(true)].isInBedRange = false;
            PlayerManager.Instance.playerDatas[characterData.GetID(true)].bedStatus = null;
            agent.speed = defaultSpeed;
        }
    }
    
    public void StandUpBed()
    {
        Vector3 nextPos = GetNextDestination();
        agent.destination = nextPos;
        targetMarkObj.transform.position = nextPos;
        SetNpcStatus(NPC_STATUS.WALK);

        if (characterData.isInBed == false) return;

        characterMover.InteractBed(characterData, false, characterData.inBedPos);
        InteractBed(false);
    }

    // デバッグ用
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            Quaternion.Euler(0f, -routineData.maxSearchAngle / 2, 0f) * transform.forward,
            routineData.maxSearchAngle,
            searchCollider.radius);

        Vector3 drawDir = transform.TransformDirection(Vector3.forward) * 5 / 2;
        Debug.DrawRay(transform.position + new Vector3(0, 1.62f, 0f), drawDir, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (npcStatus != NPC_STATUS.GO_BED)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // 周囲の敵の角度を調べる
                Vector3 targetPos = other.transform.position;
                Vector3 playerDirection = targetPos - transform.position;
                float angle = Vector3.Angle(transform.forward, playerDirection);

                // 視野角内にいれば、その敵をターゲットにする
                if (angle < routineData.maxSearchAngle)
                {
                    agent.destination = targetPos;

                    StringBuilder sb = new StringBuilder(other.gameObject.name);
                    sb.Replace("Player", "");
                    sb.Replace("Npc", "");
                    if (int.TryParse(sb.ToString(), out int id) == false) Debug.LogError("IDの変換に失敗");
                    targetData = GetChatacterData(id);

                    SetNpcStatus(NPC_STATUS.GO_ENEMY);
                }
            }
        }
    }
}