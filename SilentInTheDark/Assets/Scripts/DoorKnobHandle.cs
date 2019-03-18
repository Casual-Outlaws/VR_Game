using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnobHandle : MonoBehaviour
{
    public DoorKnob doorKnob;
    public bool isFrontSide;


    private void OnTriggerEnter( Collider other )
    {
        Debug.LogFormat( "Collided : {0}", other.gameObject.tag );
        if( other.gameObject.tag == "Player" )
        {
            doorKnob.isFrontDoor = isFrontSide;
        }
    }
}
