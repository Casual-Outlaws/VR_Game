using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnimator;

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    bool isDoorLocked;

    [SerializeField]
    bool isLevelDoor;

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
                return false;
            }
            inventory.DecreaseKey( isLevelDoor );
            isDoorLocked = false;
        }

        if( isFrontDoor )
        {
            doorAnimator.SetBool( "doorOpen", !doorAnimator.GetBool( "doorOpen" ) );
        }
        else
        {
            doorAnimator.SetBool( "doorReverseOpen", !doorAnimator.GetBool( "doorReverseOpen" ) );
        }
        return true;
    }
}
