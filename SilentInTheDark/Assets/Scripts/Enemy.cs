using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ISoundListener
{
    enum EnemyState
    {
        Idle, Walk, Attack
    }
    EnemyState currentState = EnemyState.Idle;

    [SerializeField] GameObject target, player, detectionPrefab, audioSourceOtherGO;
    [SerializeField] AudioSource audioSourceMain, audioSourceOther;
    [SerializeField] AudioClip[] stepsClips, attackClips, idleClips;
    [SerializeField] Animator modelAnim;
    [SerializeField] float speed;
    float timer, distanceToPlayer;
    bool isMoving, stopSound, isWon, isAttacked;
    public GameObject outline;
    public bool isTarget, agentActivation = true; //Reacting to sounds. Has to be public.
    public NavMeshAgent agent;
    NavMeshPath nmPath;
    Rigidbody rb;
    SphereCollider enemyCollider;
    [SerializeField] GameObject gameUI;

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
        enemyCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        isMoving = false;
        stopSound = false;
        isTarget = true;
        isWon = false;
        timer = Random.Range(1, 3);
        agent.speed = speed;
        isAttacked = false;
        if( gameUI )
            gameUI.SetActive( false );
        nmPath = new NavMeshPath();
        EventManager.Instance.RegisterEventListener( this );
    }

    void Update()
    {
        // enemy move behavior
        // 0. if game ends, don't change anything
        // 1. stay idle
        // 2. if it hears the sound, try to reach them (event)
        // 3. if the position is not reachable, just ignore it
        // 4. if there is no sound, try to move random position near him.
        // 5. if it detects the player, kill him (trigger)
        if( GameManager.Instance.gameState == GameState.LostGame )
        {
            agent.isStopped = true;
        }

        if( agent.isStopped )
        {
            timer -= Time.deltaTime;
            if( timer <= 0 )
            {
                timer = Random.Range( 2, 4 );
                bool canMove = isMovingPossible( target.transform.position );
                modelAnim.SetBool( "isIdle", !canMove );
                isMoving = canMove;

                if( canMove )
                {
                    agent.SetDestination( target.transform.position );
                    agent.isStopped = false;
                    currentState = EnemyState.Walk;
                }
                else
                {
                    currentState = EnemyState.Idle;
                }
            }
        }
        else
        {
            //Debug.LogFormat( "distance to {0} is {1}", agent.destination, agent.remainingDistance );
            if( agent.remainingDistance < agent.stoppingDistance )
            {
                agent.isStopped = true;
                modelAnim.SetBool( "isIdle", true );
                currentState = EnemyState.Idle;
            }
        }

        distanceToPlayer = transform.position.Get2DDistanceSq( player.transform.position );
        if( isAttacked && distanceToPlayer > enemyCollider.radius * enemyCollider.radius )
        {
            isAttacked = false;
            currentState = EnemyState.Idle;
        }

        PlayStateSound();

        if (agentActivation)
            agent.enabled = true;

        if (!agentActivation)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.1f);
        }

        if (distanceToPlayer < 5f)
        {
            audioSourceOtherGO.SetActive(true);
        }
        else
            audioSourceOtherGO.SetActive(false);
    }

    //checking if enemy can reach the target
    bool isMovingPossible( Vector3 targetPos )
    {
        agent.CalculatePath( targetPos, nmPath );
        return nmPath.status == NavMeshPathStatus.PathComplete;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            StartCoroutine("MoveDecision");
        }
        else if( col.gameObject.tag == "Player" && isAttacked == false )
        {
            isWon = true;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            modelAnim.SetTrigger( "attack" );
            transform.LookAt( col.gameObject.transform );
            
            EventManager.Instance.NotifyObservers( RoomEvent.PLAYER_KILLED, col.transform.position );
            isAttacked = true;
            currentState = EnemyState.Attack;
            if( gameUI )
                gameUI.SetActive( true );
        }
    }

    void PlayStateSound()
    {
        switch( currentState )
        {
            case EnemyState.Idle:
                PlayIdleSound();
                break;
            case EnemyState.Walk:
                PlayWalkSound();
                break;
            case EnemyState.Attack:
                PlayAttackSound();
                break;
        }
    }

    void PlayWalkSound()
    {
        if (!audioSourceMain.isPlaying )
        {
            audioSourceMain.volume = 1f;
            audioSourceMain.clip = stepsClips[Random.Range(0, stepsClips.Length)];
            audioSourceMain.Play();
        }

    }

    void PlayIdleSound()
    {
        if (!audioSourceMain.isPlaying)
        {
            audioSourceMain.volume = 0.2f;
            audioSourceMain.clip = idleClips[Random.Range(0, idleClips.Length)];
            audioSourceMain.Play();
        }

    }

    void PlayAttackSound()
    {
        if( !audioSourceMain.isPlaying )
        {
            audioSourceMain.volume = 0.5f;
            audioSourceMain.clip = attackClips[Random.Range( 0, attackClips.Length )];
            audioSourceMain.Play();
        }
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
        if( eventType == RoomEvent.OBJECT_THREW || 
            eventType == RoomEvent.PLYAER_TELEPORTED )
        {
            if( isMovingPossible( location ) )
            {
                float distSQ = transform.position.GetDistanceSq( location );
                //Debug.LogFormat( "Sound detected at {0}, distSq = {1}, remaining = {2}, enemy = {3}", location, distSQ, agent.remainingDistance, transform.position );
                StartCoroutine( InvestigatePosition(location) );
            }
        }
    }

    void MoveToPoint( Vector3 position )
    {
        target.transform.position = position;
        agent.SetDestination( position );
        modelAnim.SetBool( "isIdle", false );
        agent.isStopped = false;
        currentState = EnemyState.Walk;
    }

    IEnumerator InvestigatePosition( Vector3 position )
    {
        MoveToPoint( position );
        yield return null;

        bool found = false;
        while( !found )
        {
            if( agent.isStopped )
                yield break;

            // I don't know why the y-position of enemy keep decreasing
            // until we find the reason, distance is only be calculated in 2D space
            float distSQ = transform.position.Get2DDistanceSq( position );

            //Vector2 randomPlace = Random.insideUnitCircle * agent.remainingDistance * 0.2f;
            Vector2 randomPlace = Random.insideUnitCircle * distSQ * 0.1f;

            Vector3 newPos = agent.destination;
            newPos.x += randomPlace.x;
            newPos.z += randomPlace.y;
            bool canMove = isMovingPossible( newPos );
            //Debug.LogFormat( "Investigate the {0}, remainingdistance = {1}, can move ? {2}, distSq = {3}", newPos, agent.remainingDistance, canMove, distSQ );
            if( canMove )
            {
                MoveToPoint( newPos );
                found = true;
                yield break;
            }

            yield return null;
        }

    }
}
