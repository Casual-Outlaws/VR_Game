using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMove : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] NavMeshAgent agent;
    NavMeshPath path;
    [SerializeField] bool agentMoves = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agentMoves)
        agent.destination = target.transform.position;

        if (!agent.enabled)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.1f);
        }

    }
}
