using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Key : MonoBehaviour
{
    [SerializeField]
    bool isMasterKey;

    [SerializeField]
    TextMesh keyInfoText;

    [SerializeField]
    float acquirableTime = 1.5f;
    bool isAcquired = false;

    private Vector3 oldPosition;
    private Quaternion oldRotation;
    private float attachTime;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers ) & ( ~Hand.AttachmentFlags.VelocityMovement );

    private Interactable interactable;
    private Inventory playerInventory;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        playerInventory = FindObjectOfType<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if( keyInfoText )
            keyInfoText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin( Hand hand )
    {
        ShowInfoText( true );
    }

    private void HandHoverUpdate( Hand hand )
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding( this.gameObject );

        if( interactable.attachedToHand == null && startingGrabType != GrabTypes.None )
        {
            // Save our position/rotation so that we can restore it when we detach
            oldPosition = transform.position;
            oldRotation = transform.rotation;

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock( interactable );

            // Attach this object to the hand
            hand.AttachObject( gameObject, startingGrabType, attachmentFlags );
        }
        else if( isGrabEnding )
        {
            // Detach this object from the hand
            hand.DetachObject( gameObject );

            // Call this to undo HoverLock
            hand.HoverUnlock( interactable );

            // Restore position/rotation
            transform.position = oldPosition;
            transform.rotation = oldRotation;
        }
    }


    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd( Hand hand )
    {
        if( keyInfoText )
            keyInfoText.text = string.Empty;
    }

    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand( Hand hand )
    {
        //keyInfoText.text = string.Format( "Attached: {0}", hand.name );
        attachTime = Time.time;
    }



    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand( Hand hand )
    {
        //keyInfoText.text = string.Format( "Detached: {0}", hand.name );
    }


    //-------------------------------------------------
    // Called every Update() while this GameObject is attached to the hand
    //-------------------------------------------------
    private void HandAttachedUpdate( Hand hand )
    {
        ShowInfoText( false );
        if( Time.time - attachTime > acquirableTime )
        {
            if( isAcquired == false )
            {
                StartCoroutine( AcquireKey() );

                // Detach this object from the hand
                hand.DetachObject( gameObject );
                // Call this to undo HoverLock
                hand.HoverUnlock( interactable );

            }

            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach( var renderer in renderers )
            {
                Color color = renderer.material.color;
                color.a -= 0.01f;
                renderer.material.color = color;
            }
        }
        else
        {
            ShowInfoText( true );
        }
    }

    IEnumerator AcquireKey()
    {
        isAcquired = true;
        playerInventory.AddKey( isMasterKey );


        yield return new WaitForSeconds( 2.0f );
        Destroy( gameObject );
    }

    private void ShowInfoText( bool showKeyType )
    {
        if( keyInfoText )
        { 
            if( showKeyType )
            {
                if( isMasterKey )
                    keyInfoText.text = string.Format("Master Key");
                else
                    keyInfoText.text = string.Format("Door Key");
            }
            else
            {
                if( isMasterKey )
                    keyInfoText.text = string.Format( "got a master key!" );
                else
                    keyInfoText.text = string.Format( "got a door key." );
            }
        }
    }
}
