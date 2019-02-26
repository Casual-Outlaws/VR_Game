using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorKnob : MonoBehaviour
{
    [SerializeField]
    Animator doorAnimator;

    float originalAngle = 0.0f;
    [SerializeField]
    float targetAngle = 35.0f;

    CircularDrive doorHandle;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        doorHandle = GetComponent<CircularDrive>();
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //if( doorHandle.outAngle <= -targetAngle || doorHandle.outAngle >= targetAngle )
        //{
        //    if( gameObject.tag == "FrontDoor" )
        //    {
        //        StartCoroutine( DoorOpenCoroutine( true ) );
        //    }
        //    else if( gameObject.tag == "BackDoor" )
        //    {
        //        StartCoroutine( DoorOpenCoroutine( false ) );
        //    }
        //}
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
        yield return new WaitForSeconds( 0.5f );
        doorHandle.outAngle = originalAngle;
        transform.localRotation = originalRotation;
    }

    public void DoorOpenTrigger()
    {
        DoorOpenCoroutine( true );
    }

    public void DoorReverseOpenTrigger()
    {
        DoorOpenCoroutine( false );
    }
}
