using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDynamic : MonoBehaviour, ISoundListener
{
    AudioSource audioSource;
    public RippleState rippleEffect;
    bool detachedFromHand = false;

    float timer;
    [SerializeField] GameObject detectionPrefab;

    Outline hightlightShading;
    public float maxDistanceForHighlight = 6.0f;

    void Awake()
    {
        hightlightShading = GetComponent<Outline>();
        rippleEffect = FindObjectOfType<RippleState>();
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        timer = 0;
        EventManager.Instance.RegisterEventListener( this );
        if( hightlightShading )
            hightlightShading.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter( Collision col )
    {
        if( col.gameObject.tag == "ItemS" || col.gameObject.tag == "Wall" )
        {
            if( detachedFromHand == true )
            {
                audioSource.Play();
                EventManager.Instance.NotifyObservers( RoomEvent.OBJECT_THREW, gameObject.transform.position );
            }
            StartCoroutine( "Detect", timer );
        }
        else if( col.gameObject.tag == "Floor" )
        {
            if( detachedFromHand == true )
            {
                audioSource.Play();
                if( rippleEffect )
                    rippleEffect.RippleOrigin = transform.position;

                EventManager.Instance.NotifyObservers( RoomEvent.OBJECT_THREW, gameObject.transform.position );
            }
            detachedFromHand = false;
            StartCoroutine("Detect", timer);
        }
    }

    public void DetachedFromHand()
    {
        detachedFromHand = true;
    }


    IEnumerator Detect(float cooldown)
    {
        if( detectionPrefab )
            detectionPrefab.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        if( detectionPrefab )
            detectionPrefab.SetActive(false);
        timer = 0;
    }

    public void HeardSound( RoomEvent eventType, Vector3 posSound )
    {
        if( eventType == RoomEvent.OBJECT_THREW || eventType == RoomEvent.PLYAER_TELEPORTED )
        {
            float distanceSq = transform.position.GetDistanceSq( posSound );
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
