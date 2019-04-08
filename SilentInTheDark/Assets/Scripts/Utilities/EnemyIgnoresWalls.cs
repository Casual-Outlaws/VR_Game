using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIgnoresWalls : MonoBehaviour
{
    [SerializeField] int playerTPcount = 0; //How many times player teleported
    [SerializeField] float agentResetTime; //How fast to enable/disable walking thought anthing

    Enemy enemyScript;

    // Start is called before the first frame update
    void Awake()
    {
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>(); //Looking for the enemy in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTPcount == 4)
        {
            StartCoroutine("ResetNavMeshObstacles", agentResetTime); //Every X teleports enable/disable walking through anything
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


    IEnumerator ResetNavMeshObstacles(float time)
    {
        playerTPcount = 0; //Reset player teleport counter
        enemyScript.agentActivation = false;

        yield return new WaitForSeconds(time);
        enemyScript.agentActivation = true;
    }
}
