using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject targetPos;
    [SerializeField]
    int hits;
    float timeReset;
    Vector3 newPos;


    private void Awake()
    {
        targetPos = this.gameObject;
        newPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-10, 20));
        targetPos.transform.position = newPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeReset += Time.deltaTime;
        if (timeReset >= 15)
        {
            newPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-10, 20));
            targetPos.transform.position = newPos;
            timeReset = 0;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            print("hit");
            newPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-10, 20));
            targetPos.transform.position = newPos;
            hits++;
            timeReset = 0;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            newPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-10, 20));
            targetPos.transform.position = newPos;
            hits++;
            timeReset = 0;
        }
    }
}
