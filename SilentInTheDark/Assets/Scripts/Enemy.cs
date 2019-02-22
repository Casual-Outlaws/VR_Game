using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target, player;
    NavMeshAgent agent;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    bool isMoving, isWaiting, stopSound;
    float timer, distance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Target");
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isWaiting = false;
        isMoving = true;
        stopSound = false;
        timer = Random.Range(1, 3);
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

        if (isMoving)
        {
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
}
