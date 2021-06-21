using System.Text;
using UnityEngine;
using UnityEngine.AI;

public enum NPC_STATUS
{
    WALK,
    GO_ENEMY,
    GO_BED,
    PILLOW_THROW,
    LENGTH
}

public class NpcBehaviorRoutine : MonoBehaviour
{
    [SerializeField] public NpcRoutineData routineData;
    [SerializeField] public SphereCollider searchCollider;

    private CharacterData characterData;
    private CharacterMover characterMover = new CharacterMover();
    private NavMeshAgent agent;

    private CharacterData targetData;

    private GameObject targetMarkObj;

    private int npcID;

    public NPC_STATUS npcStatus = NPC_STATUS.WALK;

    private void Start()
    {
        characterData = PlayerManager.Instance.npcDatas[npcID];
        agent = GetComponent<NavMeshAgent>();
        GetNpcID();
        searchCollider.radius = routineData.warRangeToEnemy / 2;

        targetMarkObj = Instantiate(routineData.targetMark);
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

    private void SetNpcStatus(NPC_STATUS status)
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

        //Vector3 distance = agent.destination - transform.localPosition;
        //Quaternion targetRot = Quaternion.LookRotation(distance, Vector3.forward);
        //transform.rotation = targetRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (npcStatus != NPC_STATUS.WALK) return;

            Vector3 targetPos = other.transform.position;
            Vector3 playerDirection = targetPos - transform.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            if (angle < routineData.maxSearchAngle)
            {
                agent.destination = targetPos;

                StringBuilder sb = new StringBuilder(other.gameObject.name);
                sb.Replace("Player","");
                sb.Replace("Npc", "");
                if (int.TryParse(sb.ToString(), out int id) == false) Debug.LogError("IDの変換に失敗");
                targetData = GetChatacterData(id);

                agent.destination = targetData.character.transform.position;
                SetNpcStatus(NPC_STATUS.GO_ENEMY);
            }
        }
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
}
