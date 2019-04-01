using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatic : MonoBehaviour, ISoundListener
{
    Outline hightlightShading;
    public float maxDistanceForHighlight = 6.0f;

    void Awake()
    {
        hightlightShading = GetComponent<Outline>();
    }

    void Start()
    {
        EventManager.Instance.RegisterEventListener( this );
        if( hightlightShading )
            hightlightShading.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeardSound( RoomEvent eventType, Vector3 position )
    {
        if( eventType == RoomEvent.OBJECT_THREW || eventType == RoomEvent.PLYAER_TELEPORTED )
        {
            float distanceSq = transform.position.GetDistanceSq( position );
            if( distanceSq < maxDistanceForHighlight * maxDistanceForHighlight )
            {
                StartCoroutine( ChangeOutline( distanceSq / ( 2 * maxDistanceForHighlight ) ) );
            }
        }
    }

    IEnumerator ChangeOutline( float time )
    {
        yield return new WaitForSeconds( time );

        if( hightlightShading )
        {
            hightlightShading.enabled = true;
        }

        yield return new WaitForSeconds( 1.0f );
        if( hightlightShading )
        {
            hightlightShading.enabled = false;
        }
    }
}
