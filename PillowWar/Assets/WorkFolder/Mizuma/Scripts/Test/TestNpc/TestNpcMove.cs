using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNpcMove : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private NavMeshSurface[] navMeshSurfaces;

    private NavMeshAgent navMeshAgent;
    private bool isPause = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navMeshAgent.destination = target.transform.position;

        //StartCoroutine(UpdateNavMesh());
    }

    private void Update()
    {
        if(Time.frameCount % 30 == 0)
        {
            foreach (var surfaces in navMeshSurfaces)
            {
                surfaces.BuildNavMesh();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPause = !isPause;
            if (isPause) StopCoroutine(UpdateNavMesh());
            else StartCoroutine(UpdateNavMesh());
        }
    }

    IEnumerator UpdateNavMesh()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            foreach(var surfaces in navMeshSurfaces)
            {
                surfaces.BuildNavMesh();
            }
        }
    }
}
