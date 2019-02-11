using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject targetPos;
    [SerializeField]
    int hits;
    Vector3 newPos;


    private void Awake()
    {
        targetPos = this.gameObject;
        newPos = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));
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
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            print("hit");
            newPos = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));
            targetPos.transform.position = newPos;
            hits++;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            newPos = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));
            targetPos.transform.position = newPos;
            hits++;
        }
    }
}
