using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCameraDolly : MonoBehaviour
{
    public float speed = 0.1f;
    public bool x;
    //public bool z;

    // Update is called once per frame
    void Update()
    {
        if(x) transform.Translate(new Vector3(1,0,0) * Time.deltaTime * speed);
        //if(z) transform.Translate(new Vector3(0,0,1) * Time.deltaTime * speed);
    }
}
