using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorKnob : MonoBehaviour
{
    Door parentDoor;
    CircularDrive doorHandle;
    Quaternion originalRotation;
    float originalAngle = 0.0f;


    public bool isFrontDoor;
    private Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        doorHandle = GetComponent<CircularDrive>();
        parentDoor = GetComponentInParent<Door>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //originalAngle = doorHandle.startAngle;
        //originalRotation = transform.localRotation;
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

    private void HandHoverUpdate( Hand hand )
    {
        try
        {
            GrabTypes grabType = hand.GetGrabStarting();
            if( grabType != GrabTypes.None )
            {
                parentDoor.ToggleDoorOpen( isFrontDoor );
            }
        }
        catch( System.NullReferenceException e )
        {
            Debug.Log( e.ToString() );
        }
    }
}
