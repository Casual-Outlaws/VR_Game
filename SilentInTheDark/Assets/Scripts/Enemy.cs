using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target, player;
    public NavMeshAgent agent;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    bool isMoving, isWaiting, stopSound;
    public bool isTarget; //Reacting to sounds. Has to be public.
    float timer, distance;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        isWaiting = false;
        isMoving = true;
        stopSound = false;
        isTarget = true;
        timer = Random.Range(1, 3);
    }

    void Update()
    {
        //Random wait time
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

        if (isMoving)
        {
            if(isTarget)
            agent.destination = target.transform.position;
            //audioSource.PlayOneShot(audioClips[0]);
            StartCoroutine("PlaySound");
        }

        else
        {
            audioSource.Stop();
        }

        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance > 3)
        {
            stopSound = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            StartCoroutine("MoveDecision");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Item")
        {
            StartCoroutine("MoveDecision");
        }
    }

    IEnumerator PlaySound()
    {
        if (!audioSource.isPlaying && isMoving)
        {
            //audioSource.PlayOneShot(audioClips[0]);
            audioSource.clip = audioClips[0];
            audioSource.loop = true;
            audioSource.Play();
        }

        //if (audioSource.isPlaying && isMoving && distance <= 3 && stopSound == false)
        //{
        //    //audioSource.Stop();
        //    audioSource.PlayOneShot(audioClips[1]);
        //    stopSound = true;
        //}


        yield return null;
    }

    IEnumerator MoveDecision()
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
        
        yield return null;
    }
}
