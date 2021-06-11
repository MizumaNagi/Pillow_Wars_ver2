using UnityEngine;
using UnityEngine.AI;

public enum NPC_STATUS
{
    SEARCH,
    FIRE,
    GO_BED,
    NONE
}

public class NpcBehaviorRoutine : MonoBehaviour
{
    [SerializeField] private GameObject targetMark;
    [SerializeField] private Vector3 stageRange;
    [SerializeField, Range(1f, 5f)] private float searchNavMeshRange;

    private CharacterMover characterMover = new CharacterMover();
    private NavMeshAgent agent;
    private NPC_STATUS status = NPC_STATUS.NONE;
    private Transform targetTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject obj = Instantiate(targetMark);
        targetTransform = obj.transform;
    }

    private void Update()
    {
        if (agent.hasPath == false)
        {
            Vector3 pos = GetNextDestination();
            agent.destination = pos;
            targetTransform.position = pos;
        }
    }

    private Vector3 GetNextDestination()
    {
        NavMeshHit navMeshHit;

        Vector3 rndPos = new Vector3(Random.Range(-stageRange.x, stageRange.x), 0, Random.Range(-stageRange.z, stageRange.z));
        if (NavMesh.SamplePosition(rndPos, out navMeshHit, searchNavMeshRange, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
            Debug.LogError("éüÇÃñ⁄ìIínåüçıé∏îs");
            return Vector3.zero;
        }
    }
}
