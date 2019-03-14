using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target, player, detectionPrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Animator modelAnim;
    [SerializeField] float speed;
    float timer, distance;
    bool isMoving, isWaiting, canMove, stopSound;
    public GameObject outline;
    public bool isTarget; //Reacting to sounds. Has to be public.
    public NavMeshAgent agent;
    NavMeshPath nmPath;
    Rigidbody rb;

    [SerializeField] DetectionHighlight highlightScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        //modelAnim = GetComponent<Animator>();
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
        agent.speed = speed;
        nmPath = new NavMeshPath();
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
                timer = Random.Range(1, 3);
            }
        }

        //setting bool if enemy can/can't reach the target
        if (isMovingPossible() == true)
        {
            canMove = true;
        }
        else
            canMove = false;

        if (isMoving && canMove)
        {
            //modelAnim.SetBool("isIdle", false);
            if (isTarget)
            {
                agent.destination = target.transform.position;
                StartCoroutine("PlaySound");
                modelAnim.SetBool("isIdle", false);
            }
        }
        else
        {
            audioSource.Stop();
            modelAnim.SetBool("isIdle", true);
            rb.velocity = Vector3.zero;
        }

        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance > 3)
        {
            stopSound = false;
        }
    }

    //checking if enemy can reach the target
    bool isMovingPossible()
    {
        agent.CalculatePath(target.transform.position, nmPath);
        if (nmPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
            return true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Detection")
        {
            print("Detected");
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

    IEnumerator SpawnDetectionPrefab()
    {
        //outline.SetActive(true);
        //outline.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine("SpawnDetectionPrefab");
    }
}
