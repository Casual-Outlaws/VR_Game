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
        inventory = FindObjectOfType<Inventory>();
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ToggleDoorOpen( bool isFrontDoor )
    {
        if( isDoorLocked )
        {
            if( inventory.HasKey( isLevelDoor ) == false )
            {
                audio.clip = lockedSound;
                audio.Play();
                return false;
            }
            inventory.DecreaseKey( isLevelDoor );
            isDoorLocked = false;
        }

        bool isOpened;
        if( isFrontDoor )
        {
            isOpened = doorAnimator.GetBool( "doorOpen" );
            doorAnimator.SetBool( "doorOpen", !isOpened );
        }
        else
        {
            isOpened = doorAnimator.GetBool( "doorOpen" );
            doorAnimator.SetBool( "doorReverseOpen", !isOpened );
        }

        if( isOpened )
            audio.clip = closingSound;
        else
            audio.clip = openingSound;
        audio.Play();
        return true;
    }
}
