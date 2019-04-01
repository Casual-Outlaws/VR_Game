using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerControl : MonoBehaviour, ISoundListener
{
    RippleState rippleEffect;
    Teleport teleportSystem;

    void Awake()
    {
        rippleEffect = FindObjectOfType<RippleState>();
        teleportSystem = FindObjectOfType<Teleport>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterEventListener( this );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeardSound( RoomEvent eventType, Vector3 posSound )
    {
        if( eventType == RoomEvent.PLYAER_TELEPORTED )
        {
            if( rippleEffect )
                rippleEffect.RippleOrigin = transform.position;
        }
        else if( eventType == RoomEvent.PLAYER_KILLED )
        {
            if( teleportSystem )
            {
                teleportSystem.enabled = false;
            }
            GameManager.Instance.gameState = GameState.LostGame;
        }
    }
}
