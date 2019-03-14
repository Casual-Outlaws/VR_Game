using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class SimpleEnemyAI : MonoBehaviour, ISoundListener
{
    public Animator enemyModel;

    NavMeshAgent agent;
    NavMeshPath nmPath;
    GameObject player;

    UnityEvent heardSound;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag( "Player" );
        nmPath = new NavMeshPath();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterEventListener( this );
        enemyModel.SetBool( "isIdle", true );

        //agent.destination = player.transform.position;
        //agent.isStopped = false;
        //enemyModel.SetBool( "isIdle", false );
    }

    // Update is called once per frame
    void Update()
    {
        if( GameManager.Instance.gameState == GameState.LostGame )
        {
            agent.isStopped = true;
            return;
        }

        //Vector3 direction = player.transform.position - gameObject.transform.position;
        //Ray ray = new Ray( gameObject.transform.position, direction );
        //RaycastHit hit;

        if( agent.remainingDistance < agent.stoppingDistance )
        {
            agent.isStopped = true;
            enemyModel.SetBool( "isIdle", true );
        }

        //if( Physics.Raycast( ray, out hit ) )
        //{
        //    if( hit.collider.tag == "Player" )
        //    {
        //        agent.destination = player.transform.position;
        //        agent.isStopped = false;
        //        enemyModel.SetBool( "isIdle", false );
        //    }
        //    else
        //    {
        //        agent.isStopped = true;
        //        enemyModel.SetBool( "isIdle", true );
        //    }
        //}
    }

    public void HeardSound( Vector3 location )
    {
        if( agent.CalculatePath( location, nmPath ) )
        {
            agent.destination = location;
            agent.isStopped = false;
            enemyModel.SetBool( "isIdle", false );
        }
    }
}
