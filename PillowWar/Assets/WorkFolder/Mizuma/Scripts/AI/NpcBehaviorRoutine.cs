using System.Text;
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

    private void GetNpcID()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        sb.Replace("Npc", "");
        if (int.TryParse(sb.ToString(), out npcID) == false) Debug.LogError("NpcID取得失敗");
        sb.Clear();
    }

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

    private Vector3 GetShortestBedPos()
    {
        float shortestBedPos = Mathf.Infinity;
        int shortIndex = -1;
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

    private BedStatus GetDestinationBedStatus(int index)
    {
        return BedManager.Instance.bedColliders[index].GetComponent<BedStatus>();
    }

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
                    if (agent.hasPath == false) Debug.Log("布団到着");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void UpdateActivity()
    {
        switch (npcStatus)
        {
            case NPC_STATUS.WALK:
                {
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
                    if (targetData.HP <= 0)
                    {
                        ResetTarget(); 
                        break;
                    }
                    agent.destination = targetData.character.transform.position;
                    LookAtTarget();

                    if(agent.hasPath == false)
                    {
                        Debug.LogWarning("経路なし");
                        SetNpcStatus(NPC_STATUS.WALK);
                        break;
                    }
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
                            StandUpBed();
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

    private void LookAtTarget()
    {
        transform.LookAt(agent.destination);
    }

    private CharacterData GetChatacterData(int id)
    {
        if (id < 100) return PlayerManager.Instance.playerDatas[id];
        else return PlayerManager.Instance.npcDatas[id - 100];
    }

    private void ResetTarget()
    {
        targetData = null;
        SetNpcStatus(NPC_STATUS.WALK);
    }

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

        //Debug.Log($"※ 襲撃イベント\n失敗値: {bedEventThroughPercent}, 成功値: {rnd} + {routineData.minStartGoBedPercent}, 結果: {bedEventThroughPercent < rnd + routineData.minStartGoBedPercent}");
        if (bedEventThroughPercent > rnd + routineData.minStartGoBedPercent) 
        {
            SetNpcStatus(NPC_STATUS.WALK);
            return; 
        }

        Vector3 nextPos = GetShortestBedPos();
        agent.destination = nextPos;
        targetMarkObj.transform.position = nextPos;
    }

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
        characterMover.InteractBed(characterData, false, characterData.inBedPos);
        InteractBed(false);
        SetNpcStatus(NPC_STATUS.WALK);
        Vector3 nextPos = GetNextDestination();
        agent.destination = nextPos;
        targetMarkObj.transform.position = nextPos;
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
                Vector3 targetPos = other.transform.position;
                Vector3 playerDirection = targetPos - transform.position;
                float angle = Vector3.Angle(transform.forward, playerDirection);
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