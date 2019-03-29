using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, ISoundListener
{
    RippleState rippleEffect;

    void Awake()
    {
        rippleEffect = FindObjectOfType<RippleState>();
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
    }
}
