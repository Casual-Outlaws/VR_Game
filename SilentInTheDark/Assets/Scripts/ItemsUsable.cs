using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUsable : MonoBehaviour
{
    #region Variables to interact with enemy
    GameObject enemyPrefab;
    Enemy enemyScript;
    [SerializeField]
    float distance;
    #endregion

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    void Start()
    {
        enemyPrefab = GameObject.FindGameObjectWithTag("Enemy");
        enemyScript = enemyPrefab.GetComponent<Enemy>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (!enemyPrefab)
        //{
        //    enemyPrefab = GameObject.FindGameObjectWithTag("Enemy");

        //}

        distance = Vector3.Distance(this.transform.position, enemyPrefab.transform.position); //I'm not sure if we need update at all. I need to do more tests with collision.
    }

    private void OnCollisionEnter(Collision col)
    {
        //Logic #1 for enemy reaction
        if(col.gameObject.tag == "Floor" && distance <= 4)
        {
            enemyScript.isTarget = false;
            enemyScript.agent.destination = this.transform.position;
        }
    }
}
