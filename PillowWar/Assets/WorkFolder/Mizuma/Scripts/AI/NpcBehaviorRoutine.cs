using System.Text;
using UnityEngine;
using UnityEngine.AI;

public enum NPC_STATUS
{
    WALK,
    GO_ENEMY,
    GO_BED,
    THROW,
    LENGTH
}

public class NpcBehaviorRoutine : MonoBehaviour
{
    [SerializeField] public NpcRoutineData routineData;
    [SerializeField] public SphereCollider searchCollider;

    private CharacterMover characterMover = new CharacterMover();
    private NavMeshAgent agent;
    private Transform targetTransform;

    private int npcID;

    public NPC_STATUS npcStatus = NPC_STATUS.WALK;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNpcID();

        GameObject obj = Instantiate(routineData.targetMark);
        targetTransform = obj.transform;
    }

    private void Update()
    {
        if (GameManager.Instance.isPause == false)
        {
            UpdateActivity();
        }
    }

    private void GetNpcID()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        sb.Replace("Npc", "");
        if (int.TryParse(sb.ToString(), out npcID) == false) Debug.LogError("NpcID�̎擾���s�ANpcObj�����m�F���Ă��������B");
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
            Debug.LogError("���̖ړI�n�������s");
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
                    agent.stoppingDistance = routineData.warRangeWithEnemy;
                    break;
                }
            case NPC_STATUS.GO_BED:
                {
                    break;
                }
            case NPC_STATUS.THROW:
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
                        Vector3 pos = GetNextDestination();
                        agent.destination = pos;
                        targetTransform.position = pos;
                        SetNpcStatus(NPC_STATUS.WALK);
                    }
                    break;
                }
            case NPC_STATUS.GO_ENEMY:
                {
                    if (agent.hasPath == false)
                    {
                        SetNpcStatus(NPC_STATUS.THROW);
                    }
                    break;
                }
            case NPC_STATUS.GO_BED:
                {
                    break;
                }
            case NPC_STATUS.THROW:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Vector3 targetPos = other.transform.position;
            Vector3 playerDirection = targetPos - transform.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            if (angle < routineData.maxSearchAngle)
            {
                agent.destination = targetPos;
                targetTransform.position = targetPos;
                SetNpcStatus(NPC_STATUS.GO_ENEMY);
            }
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            Quaternion.Euler(0f, -routineData.maxSearchAngle / 2, 0f) * transform.forward,
            routineData.maxSearchAngle,
            searchCollider.radius);
        Debug.Log(Quaternion.Euler(0f, -routineData.maxSearchAngle, 0f) * transform.forward);
    }
}
