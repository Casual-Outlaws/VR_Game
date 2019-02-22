using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] GameObject targetPos, playerPos; //Object's position
    [SerializeField] int hits; //Debug variable to check collision
    float timeReset; //In case if the enemy can't reach this object for X seconds new position will be generated
    Vector3 newPos; //new coordinates


    private void Awake()
    {
        targetPos = this.gameObject;
        playerPos = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("generatePosition");
        targetPos.transform.position = newPos;
        hits = 0;
    }

    void Update()
    {
        timeReset += Time.deltaTime;
        if (timeReset >= 10)
        {
            StartCoroutine("generatePosition");
            targetPos.transform.position = newPos;
            timeReset = 0;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            print("hit");
            StartCoroutine("generatePosition");
            targetPos.transform.position = newPos;
            hits++;
            timeReset = 0;
        }
    }

    IEnumerator generatePosition()
    {
        float coin = Random.Range(0f, 10f);
        print(coin);
        if (coin <= 7.49f)
        {
            newPos = new Vector3(Random.Range(-8, 5), 1, Random.Range(-5, 10));
        }
        else
        {
            newPos = playerPos.transform.position;
        }
        yield return null;
    }
}
