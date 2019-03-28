using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum GameState
{
    InMenu,
    InTutorial,
    InPlaying,
    LostGame,
    WonGame,
};

public class GameManager : MonoBehaviour
{
    public GameState gameState { get; set; }

    public static GameManager Instance = null;

    [SerializeField] int playerTPcount = 0;
    [SerializeField] GameObject[] navmeshGO;

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else if( Instance != this )
        {
            UnityEngine.Object.Destroy( gameObject );
        }

        DontDestroyOnLoad( gameObject );
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.InPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTPcount == 4)
        {
            if (navmeshGO != null)
            {
                for (int i = 0; i < navmeshGO.Length; i++)
                {
                    navmeshGO[i].GetComponent<NavMeshObstacle>().carving = false;
                }

                StartCoroutine("ResetNavMeshObstacles");
            }
            else
            {
                print("NavMesh Game Object array is NULL");
            }
        }
    }

    public void HeardSound(RoomEvent eventType, Vector3 location)
    {
        if (eventType == RoomEvent.OBJECT_THREW)
        {
            // start to investigate where the sound come
            //Debug.LogFormat( "heard sound at {0}", location );
        }
        else if (eventType == RoomEvent.PLYAER_TELEPORTED)
        {
            // increment player teleportation count here
            // ex. numPlayerTeleport += 1;
            //Debug.LogFormat( "player teleport to {0}", location );

            playerTPcount += 1;
        }
    }

    IEnumerator ResetNavMeshObstacles()
    {
        float timer = 0;
        timer += Time.deltaTime;
        if(timer == 10f)
        {
            for (int i = 0; i < navmeshGO.Length; i++)
            {
                navmeshGO[i].GetComponent<NavMeshObstacle>().carving = true;
            }
            StopCoroutine("ResetNavMeshObstacles");
        }
            
        yield return null;
    }
}
