﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorKnob : MonoBehaviour
{
    [SerializeField]
    Animator doorAnimator;

    CircularDrive doorHandle;
    Quaternion originalRotation;
    float originalAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        doorHandle = GetComponent<CircularDrive>();
        originalAngle = doorHandle.startAngle;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DoorOpenCoroutine( bool frontDoor )
    {
        if( frontDoor )
        {
            doorAnimator.SetBool( "doorOpen", !doorAnimator.GetBool( "doorOpen" ) );
        }
        else
        {
            doorAnimator.SetBool( "doorReverseOpen", !doorAnimator.GetBool( "doorReverseOpen" ) );
        }
        doorHandle.outAngle = originalAngle;
        yield return new WaitForSeconds( 0.5f );
        doorHandle.outAngle = originalAngle;
        transform.localRotation = originalRotation;
    }

    public void DoorOpenTrigger()
    {
        StartCoroutine( DoorOpenCoroutine( true ) );
    }

    public void DoorReverseOpenTrigger()
    {
        StartCoroutine( DoorOpenCoroutine( false ) );
    }
}
