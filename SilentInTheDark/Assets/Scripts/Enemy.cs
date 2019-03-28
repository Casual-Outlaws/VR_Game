using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ISoundListener
{
    [SerializeField] GameObject target, player, detectionPrefab, audioSourceOtherGO;
    [SerializeField] AudioSource audioSourceMain, audioSourceOther;
    [SerializeField] AudioClip[] stepsClips, attackClips, idleClips;
    [SerializeField] Animator modelAnim;
    [SerializeField] float speed;
    float timer, distanceToPlayer;
    bool isMoving, canMove, stopSound, isWon;
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
        audioSourceMain = GetComponent<AudioSource>();
        audioSourceOther = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        isMoving = true;
        stopSound = false;
        isTarget = true;
        isWon = false;
        timer = Random.Range(1, 3);
        agent.speed = speed;
        nmPath = new NavMeshPath();
        EventManager.Instance.RegisterEventListener( this );
    }

    void Update()
    {
        //Random wait time
        if (!canMove)
        {
            StartCoroutine("PlayIdleSound");
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            if(!isWon)
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isMoving = true;
                timer = Random.Range(1, 3);
            }
        }
        else
            agent.isStopped = false;

        //setting bool if enemy can/can't reach the target
        if (isMovingPossible() == true)
        {
            canMove = true;
        }
        else
            canMove = false;

        if (isMoving && canMove)
        {
            if (isTarget)
            {
                agent.destination = target.transform.position;
                StartCoroutine("PlaySound");
                modelAnim.SetBool("isIdle", false);
            }
        }
        else
        {
            modelAnim.SetBool("isIdle", true);
            rb.velocity = Vector3.zero;
        }

        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        //print(distanceToPlayer); need to look into this more
        if (distanceToPlayer < 4f)
        {
            audioSourceOtherGO.SetActive(true);
        }
        else
            audioSourceOtherGO.SetActive(false);
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
        if (col.gameObject.tag == "Target")
        {
            StartCoroutine("MoveDecision");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isWon = true;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            modelAnim.SetBool("isAttack", true);
        }
    }

    IEnumerator PlaySound()
    {
        if (!audioSourceMain.isPlaying && isMoving)
        {
            audioSourceMain.volume = 1f;
            audioSourceMain.clip = stepsClips[Random.Range(0, stepsClips.Length)];
            audioSourceMain.Play();
        }
        yield return null;
    }

    IEnumerator PlayIdleSound()
    {
        if (!audioSourceMain.isPlaying)
        {
            audioSourceMain.volume = 0.2f;
            audioSourceMain.clip = idleClips[Random.Range(0, idleClips.Length)];
            audioSourceMain.Play();
        }
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
        
        yield return null;
    }

    IEnumerator SpawnDetectionPrefab()
    {
        //outline.SetActive(true);
        //outline.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine("SpawnDetectionPrefab");
    }

    public void HeardSound( RoomEvent eventType, Vector3 location )
    {
        if( eventType == RoomEvent.OBJECT_THREW )
        {
            // start to investigate where the sound come
            //Debug.LogFormat( "heard sound at {0}", location );
        }
        else if( eventType == RoomEvent.PLYAER_TELEPORTED )
        {
            // increment player teleportation count here
            // ex. numPlayerTeleport += 1;
            //Debug.LogFormat( "player teleport to {0}", location );
        }
    }

}
