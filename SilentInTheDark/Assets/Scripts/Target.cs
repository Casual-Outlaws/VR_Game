using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject playerPos; //Object's position
    [SerializeField] float posXmin, posXmax, posZmin, posZmax;
    public GameObject targetPos;
    public bool forceChange;
    float timeReset; //In case if the enemy can't reach this object for X seconds new position will be generated or if targetPos was forceChanged.
    Vector3 newPos; //new coordinates


    private void Awake()
    {
        targetPos = this.gameObject;
        playerPos = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("generatePosition");
        targetPos.transform.position = newPos;
        forceChange = false; //[forceChange = true] CAN ONLY BE SET BY DETECTION FROM ANOTHER OBJECT
    }

    void Update()
    {
        timeReset += Time.deltaTime;
        if (forceChange && timeReset >= 20)
        {
            StartCoroutine("generatePosition");
            targetPos.transform.position = newPos;
            timeReset = 0;
            forceChange = false;
        }

        if (!forceChange && timeReset >= 10)
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
            //print("hit");
            StartCoroutine("generatePosition");
            targetPos.transform.position = newPos;
            timeReset = 0;
        }
    }

    IEnumerator generatePosition()
    {
        float coin = Random.Range(0f, 10f);
        //print(coin);
        if (coin <= 7.49f)
        {
            newPos = new Vector3(Random.Range(posXmin, posXmax), 1, Random.Range(posZmin, posZmax));
        }
        else
        {
            newPos = playerPos.transform.position;
        }
        yield return null;
    }
}

//Это было не легко, но логику я осуществил. Абсолютная дичь, но результат стоил моих усилий.
