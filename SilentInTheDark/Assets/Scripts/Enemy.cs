using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject target, player;
    NavMeshAgent agent;
    [SerializeField]
    AudioSource audioSource;
    bool isMoving, isWaiting;
    float timer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Target");
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isWaiting = false;
        isMoving = true;
        timer = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isMoving = true;
                isWaiting = false;
                timer = Random.Range(1, 5);
            }
        }

        if(isMoving)
        agent.destination = target.transform.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            isMoving = false;
            int roll = Random.Range(0, 9);
            if (roll >= 5)
            {
                isMoving = true;
            }
            else
            {
                isWaiting = true;
            }
        }
    }


}
