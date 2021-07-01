using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceUpdate : MonoBehaviour
{
    [SerializeField] private int UpdateFrameCT;
    private NavMeshSurface navMesh;

    private void Start()
    {
        navMesh = GetComponent<NavMeshSurface>();
    }

    private void Update()
    {
        if (Time.deltaTime % UpdateFrameCT == 0 && GameManager.Instance.isPause == false) navMesh.BuildNavMesh();
    }
}
