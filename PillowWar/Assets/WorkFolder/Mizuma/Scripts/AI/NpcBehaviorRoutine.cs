using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public enum NPC_STATUS
{
    IS_READY,
    WALK,
    GO_ENEMY,
    ESCAPE,
    GO_BED,
    IN_BED,
    PILLOW_THROW,
    STUN,
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
    private float defaultSpeed;
    private Vector3 beforeFramePos;

    public float startGoBedTime;
    public bool isOnceGoBed;

    public int npcID;

    private float escapeTime = 2;
    private float remainEscapeTime = 0;

    private void Start()
    {
        beforeFramePos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        GetNpcID();
        characterData = PlayerManager.Instance.npcDatas[npcID - 100];
        searchCollider.radius = routineData.warRangeToEnemy / 2;

        GameEventScript.Instance.npcBehaviorRoutines.Add(this);
        defaultSpeed = agent.speed;
        startGoBedTime = routineData.minStartGoBedTime;

        SetNpcStatus(NPC_STATUS.IS_READY);
    }

    private void Update()
    {
        if (CanMoveing() == true) agent.speed = defaultSpeed;
        else
        {
            transform.position = beforeFramePos;
            agent.speed = 0;
            return;
        }

        beforeFramePos = transform.position;

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

    private bool CanMoveing()
    {
        if (GameManager.Instance.isPlayTheGame == false) return false;
        else if (characterData.remainStunTime > 0) return false;
        else if (GameEventScript.Instance.canAction == false) return false;
        else if (GameManager.Instance.isPause == true) return false;
        else return true;
    }

    private IEnumerator ResumeNavmeshAgent()
    {
        yield return new WaitForSeconds(0.5f);
        agent.isStopped = false;
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

        for(int i = 0; i < routineData.searchMaxCount; i++)
        {
            Vector3 rndPos = new Vector3(
                UnityEngine.Random.Range(routineData.negativeStageRange.x, routineData.positiveStageRange.x),
                0f,
                UnityEngine.Random.Range(routineData.negativeStageRange.z, routineData.positiveStageRange.z));
            if (NavMesh.SamplePosition(rndPos, out navMeshHit, routineData.searchNavMeshRange, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }
        }

        Debug.LogWarning("Ai目的地の設定失敗");
        return Vector3.zero;
    }

    /// <summary>
    /// 最も近い未使用ベッドの座標を取得する
    /// </summary>
    /// <returns>最も近い未使用ベッドの座標</returns>
    private Vector3 GetShortestBedPos()
    {
        float shortestBedPos = Mathf.Infinity;
        int shortIndex = -1;

        // BedManager.Instanceに登録されている全ての未使用ベッドから最も近いベッドのインデックス番号を取得する
        for (int i = 0; i < BedManager.Instance.bedColliders.Count; i++)
        {
            if (BedManager.Instance.bedColliders[i].gameObject.activeSelf == false) continue;

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
        return BedManager.Instance.bedColliders[index].GetComponentInParent<BedStatus>();
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

                    Vector3 nextPos = GetNextDestination();
                    agent.destination = nextPos;
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
            case NPC_STATUS.ESCAPE:
                {
                    if (remainEscapeTime < 0) return;
                    remainEscapeTime = escapeTime;

                    int x = targetData.myBodyTransform.position.x < transform.position.x ? 1 : -1;
                    int y = targetData.myBodyTransform.position.y < transform.position.y ? 1 : -1;
                    agent.destination = new Vector3(7 * x, 0, 7 * y);

                    break;
                }
            case NPC_STATUS.STUN:
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
                    if (agent.hasPath == false && agent.pathPending == false)
                    {
                        Vector3 nextPos = GetNextDestination();
                        agent.destination = nextPos;
                    }
                    break;
                }
            case NPC_STATUS.GO_ENEMY:
            case NPC_STATUS.PILLOW_THROW:
                {
                    // 攻撃対象が死んだら別ターゲットを探しに行く
                    // 攻撃対象がベッドに入ったら別ターゲットを探しに行く
                    if (targetData.HP <= 0 || targetData.isInBed)
                    {
                        ResetTarget();
                        break;
                    }

                    if (agent.hasPath == false)
                    {
                        break;
                    }

                    // ターゲットの座標更新
                    agent.destination = targetData.character.transform.position;
                    LookAtTarget();

                    // 接敵距離まで接近したら枕を投げる
                    if (agent.remainingDistance <= routineData.warRangeToEnemy)
                    {
                        if (characterData.remainthrowCT < 0 && characterData.isHavePillow) characterMover.PillowThrow(characterData);
                    }
                    break;
                }
            case NPC_STATUS.GO_BED:
                {
                    if (characterData.bedStatus == null)
                    {
                        Debug.LogWarning("布団消失");
                        SetNpcStatus(NPC_STATUS.WALK);
                        break;
                    }

                    if (characterData.bedStatus.canIn == false)
                    {
                        SetNpcStatus(NPC_STATUS.WALK);
                    }
                    break;
                }
            case NPC_STATUS.IN_BED:
                {
                    break;
                }
            case NPC_STATUS.ESCAPE:
                {
                    remainEscapeTime -= Time.deltaTime;
                    if (remainEscapeTime < 0)
                    {
                        SetNpcStatus(NPC_STATUS.WALK);
                        break;
                    }

                    //Vector3 dest = EscapeTarget(transform.localPosition, targetData.myBodyTransform.localPosition);
                    //agent.destination = dest;
                    //targetMarkObj.transform.position = dest;
                    //Debug.Log(dest);
                    break;
                }
            case NPC_STATUS.STUN:
                {
                    if (characterData.remainStunTime < 0) SetNpcStatus(NPC_STATUS.WALK);
                    break;
                }
            case NPC_STATUS.IS_READY:
                {
                    SetNpcStatus(NPC_STATUS.WALK);
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
    /// 襲撃イベントに備えるか、無視するか
    /// (HP割合は実行確立に影響)
    /// </summary>
    public void TriggerGoBed()
    {
        // 残りHP割合が襲撃イベ開始HP割合を下回っていなければイベント無視
        float remainHpPercent = ((float)characterData.HP / (float)GameManager.Instance.ruleData.maxHp) * 100;
        if (remainHpPercent >= routineData.startGoBedRemHpPercent)
        {
            //SetNpcStatus(NPC_STATUS.WALK);
            return;
        }

        // 残りHP割合を元にイベント実行率を計算、イベント実行率よりもRandom(0,100)の方が多ければイベント無視
        float bedEventThroughPercent = Mathf.Clamp01(remainHpPercent / routineData.startGoBedRemHpPercent) * 100;
        int rnd = UnityEngine.Random.Range(0, 100);

        Debug.Log($"※ 襲撃イベント\n失敗値: {bedEventThroughPercent}, " +
            $"成功値: {rnd} + {routineData.minStartGoBedPercent}, " +
            $"結果: {bedEventThroughPercent < rnd + routineData.minStartGoBedPercent}");
        if (bedEventThroughPercent > rnd + routineData.minStartGoBedPercent)
        {
            //SetNpcStatus(NPC_STATUS.WALK);
            return;
        }

        // 座標を取得、目的地に設定
        Vector3 nextPos = GetShortestBedPos();
        agent.destination = nextPos;

        SetNpcStatus(NPC_STATUS.GO_BED);
    }

    /// <summary>
    /// 襲撃イベントに備えるか、無視するか
    /// (HP割合は開始時間に影響(ベッドに向かう確率100%))
    /// </summary>
    public void CheckTimeTriggerGoBed()
    {
        if (isOnceGoBed == true) return;

        isOnceGoBed = true;
        Vector3 nextPos = GetShortestBedPos();
        if(nextPos == Vector3.zero || characterData.bedStatus == null)
        {
            SetNpcStatus(NPC_STATUS.WALK);
            return;
        }
        agent.destination = nextPos;

        SetNpcStatus(NPC_STATUS.GO_BED);
    }

    public void ResetBedEventStatus()
    {
        UpdateStartGoBedTime();
    }

    /// <summary>
    /// ベッドに入退出時の処理
    /// </summary>
    /// <param name="isInBed">ベッドに入ろうとしているか</param>
    public void InteractBed(bool isInBed)
    {
        try
        {
            if (isInBed)
            {
                agent.destination = characterData.inBedPos;
                agent.speed = 0;
            }
            else
            {
                PlayerManager.Instance.npcDatas[characterData.GetID(true)].isInBedRange = false;
                PlayerManager.Instance.npcDatas[characterData.GetID(true)].bedStatus = null;
                agent.speed = defaultSpeed;
            }
        }
        catch (System.ArgumentOutOfRangeException e) { Debug.LogError(characterData.GetID(true)); Debug.LogError(e.ParamName); }
    }

    public void StandUpBed()
    {
        Vector3 nextPos = GetNextDestination();
        agent.destination = nextPos;
        SetNpcStatus(NPC_STATUS.WALK);

        if (characterData.isInBed == false) return;

        characterMover.InteractBed(characterData, false, characterData.inBedPos);
        InteractBed(false);
    }

    private int DamagedChgRoutine()
    {
        int rnd = UnityEngine.Random.Range(0, 100);
        int deadValue = 0;

        if (deadValue + routineData.shootingDamageChgTargetRoutinePercent > rnd)
        {
            deadValue += (int)routineData.shootingDamageChgTargetRoutinePercent;
            return (int)NPC_STATUS.GO_ENEMY;
        }
        else if (deadValue + routineData.shootingDamageChgEscapeRoutinePercent > rnd)
        {
            deadValue += (int)routineData.shootingDamageChgEscapeRoutinePercent;
            return (int)NPC_STATUS.ESCAPE;
        }
        else if (deadValue + routineData.shootingDamageChgEscapeAndJumpRoutinePercent > rnd)
        {
            return -1;
        }
        else return -1;
    }

    private void UpdateStartGoBedTime()
    {
        isOnceGoBed = false;

        float remainHpPercent = ((float)characterData.HP / (float)GameManager.Instance.ruleData.maxHp);
        if (remainHpPercent >= routineData.startGoBedRemHpPercent)
        {
            startGoBedTime = routineData.minStartGoBedTime;
            return;
        }
        float minStartGoBedTime = routineData.minStartGoBedTime;
        float maxStartGoBedTime = routineData.maxStartGoBedTime;

        startGoBedTime = minStartGoBedTime + ((1 - remainHpPercent) * (maxStartGoBedTime - minStartGoBedTime));
    }

    private Vector3 EscapeTarget(Vector3 selfPos, Vector3 targetPos)
    {
        Vector3 vec = targetPos - selfPos;
        //Vector3 dest = new Vector3(vec.x * -1, 0, vec.z * -1);
        Vector3 dest = Vector3.Scale(new Vector3(vec.x * 1, 0, vec.z * 1), -transform.forward);
        Vector3 result = dest + selfPos;
        result.y = 0;

        return result;
    }

    // デバッグ用
    //private void OnDrawGizmos()
    //{
    //    UnityEditor.Handles.color = Color.green;
    //    UnityEditor.Handles.DrawSolidArc(
    //        transform.position,
    //        Vector3.up,
    //        Quaternion.Euler(0f, -routineData.maxSearchAngle / 2, 0f) * transform.forward,
    //        routineData.maxSearchAngle,
    //        searchCollider.radius);
    //
    //    Vector3 drawDir = transform.TransformDirection(Vector3.forward) * 5 / 2;
    //    Debug.DrawRay(transform.position + new Vector3(0, 1.62f, 0f), drawDir, Color.red);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (npcStatus != NPC_STATUS.IN_BED && npcStatus != NPC_STATUS.GO_BED)
        {
            if (collision.gameObject.tag == "Pillow")
            {
                int pillowNum = int.Parse(collision.gameObject.name);
                if (pillowNum == npcID) return;

                // ルーチンを変更するか決める
                int result = DamagedChgRoutine();
                if (result == -1) return;

                // ターゲット変更
                if (result == (int)NPC_STATUS.GO_ENEMY)
                {
                    int ID = int.Parse(collision.gameObject.name);
                    if (ID < 100) targetData = PlayerManager.Instance.playerDatas[ID];
                    else targetData = PlayerManager.Instance.npcDatas[ID - 100];
                    SetNpcStatus(NPC_STATUS.GO_ENEMY);
                }

                // 逃走開始
                if (result == (int)NPC_STATUS.ESCAPE)
                {
                    int index = int.Parse(collision.gameObject.name);
                    if (index < 100) targetData = PlayerManager.Instance.playerDatas[index];
                    else targetData = PlayerManager.Instance.npcDatas[index - 100];
                    SetNpcStatus(NPC_STATUS.ESCAPE);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (PlayerManager.Instance.npcDatas[npcID - 100].isInBed == false)
        if(npcStatus != NPC_STATUS.GO_BED && npcStatus != NPC_STATUS.IN_BED && npcStatus != NPC_STATUS.ESCAPE)
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
                    if (int.TryParse(sb.ToString(), out int id) == false)
                    {
                        Debug.LogError("IDの変換に失敗 / " + sb.ToString());
                    }
                    targetData = GetChatacterData(id);

                    SetNpcStatus(NPC_STATUS.GO_ENEMY);
                }
            }
        }
    }
}
