using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNpcMove : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navMeshAgent.destination = target.transform.position;
    }

    private void Update()
    {
    }
}
