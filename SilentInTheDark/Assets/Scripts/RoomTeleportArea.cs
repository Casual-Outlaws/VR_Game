using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RoomTeleportArea : Valve.VR.InteractionSystem.TeleportArea
{
    public override void TeleportPlayer( Vector3 pointedAtPosition )
    {
        Debug.LogFormat( "Player Teleported to {0}", pointedAtPosition );
        EventManager.Instance.NotifyObservers( RoomEvent.PLYAER_TELEPORTED, pointedAtPosition );
    }

}
