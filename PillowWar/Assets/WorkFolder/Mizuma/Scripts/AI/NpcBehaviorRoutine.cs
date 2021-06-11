using System.Text;
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
    [SerializeField] private NpcRoutineData routineData;

    private CharacterMover characterMover = new CharacterMover();
    private NavMeshAgent agent;
    private Transform targetTransform;

    private int npcID;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNpcID();

        GameObject obj = Instantiate(routineData.targetMark);
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

    private void GetNpcID()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        sb.Replace("Npc", "");
        if (int.TryParse(sb.ToString(), out npcID) == false) Debug.LogError("NpcIDの取得失敗、NpcObj名を確認してください。");
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
            Debug.LogError("次の目的地検索失敗");
            return Vector3.zero;
        }
    }
}
