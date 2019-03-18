using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnimator;

    Inventory inventory;

    [SerializeField]
    bool isDoorLocked;

    [SerializeField]
    bool isLevelDoor;

    [SerializeField]
    AudioClip lockedSound;

    [SerializeField]
    AudioClip openingSound;

    [SerializeField]
    AudioClip closingSound;


    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        doorAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ToggleDoorOpen( bool isFrontDoor )
    {
        if( isFrontDoor )
            Debug.Log( "Front door Toggle." );
        else
            Debug.Log( "Back door Toggle." );
        bool canOpen = true;
        if( isDoorLocked )
        {
            bool hasKey = inventory.HasKey( isLevelDoor );
            doorAnimator.SetBool( "isDoorLocked", !hasKey );

            if( hasKey == false )
            {
                audio.clip = lockedSound;
                audio.Play();
                canOpen = false;
            }
            else
            {
                inventory.DecreaseKey( isLevelDoor );
                isDoorLocked = false;
            }
        }
        doorAnimator.SetTrigger( "ToggleOpenDoor" );

        if( canOpen )
        {
            bool isOpened;
            doorAnimator.SetBool( "isFrontDoor", isFrontDoor );
            if( isFrontDoor )
            {
                isOpened = doorAnimator.GetBool( "doorOpen" );
                doorAnimator.SetBool( "doorOpen", !isOpened );
            }
            else
            {
                isOpened = doorAnimator.GetBool( "doorReverseOpen" );
                doorAnimator.SetBool( "doorReverseOpen", !isOpened );
            }

            if( isOpened )
                audio.clip = closingSound;
            else
                audio.clip = openingSound;
            audio.Play();
        }
        return canOpen;
    }
}
