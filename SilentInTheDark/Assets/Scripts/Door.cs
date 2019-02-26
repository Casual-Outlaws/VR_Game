using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpenTrigger()
    {
        doorAnimator.SetBool( "doorOpen", !doorAnimator.GetBool( "doorOpen" ) );
    }

    public void DoorReverseOpenTrigger()
    {
        doorAnimator.SetBool( "doorReverseOpen", !doorAnimator.GetBool( "doorReverseOpen" ) );
    }
}
