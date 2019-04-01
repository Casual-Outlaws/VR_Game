using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public enum RoomEvent
{
    OBJECT_THREW,
    PLYAER_TELEPORTED,
    PLAYER_KILLED,
}

public interface ISoundListener
{
    void HeardSound( RoomEvent eventType, Vector3 position );
}

