using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorKnob : MonoBehaviour
{
    [SerializeField]
    Animator doorAnimator;

    Door parentDoor;
    CircularDrive doorHandle;
    Quaternion originalRotation;
    float originalAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        doorHandle = GetComponent<CircularDrive>();
        parentDoor = GetComponentInParent<Door>();
        originalAngle = doorHandle.startAngle;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DoorOpenCoroutine( bool frontDoor )
    {
        bool opened = parentDoor.ToggleDoorOpen( frontDoor );
        if( opened == false )
            yield break;
        
        doorHandle.rotateGameObject = false;
        transform.localRotation = originalRotation;
        doorHandle.outAngle = originalAngle;

        yield return new WaitForSeconds( 0.5f );
        doorHandle.outAngle = originalAngle;
        transform.localRotation = originalRotation;
        doorHandle.rotateGameObject = true;
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
