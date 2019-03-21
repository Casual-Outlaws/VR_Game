﻿using System.Collections;
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

    public float maxDistanceForHighlight = 10.0f;

    void Awake()
    {
        hightlightShading = GetComponent<Outline>();

        rippleEffect = FindObjectOfType<RippleState>();
    }


    void Start()
    {
        timer = 0;
        audioSource = GetComponent<AudioSource>();
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
        if( col.gameObject.tag == "Floor" )
        {
            if( detachedFromHand == true )
            {
                audioSource.Play();
                if( rippleEffect )
                    rippleEffect.RippleOrigin = transform.position;

                EventManager.Instance.NotifyObservers( gameObject.transform.position );
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
        detectionPrefab.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        detectionPrefab.SetActive(false);
        timer = 0;
    }

    public void HeardSound( Vector3 posSound )
    {
        float distanceSq = gameObject.transform.position.GetDistanceSq( posSound );
        if( distanceSq < maxDistanceForHighlight * maxDistanceForHighlight )
        {
            StartCoroutine( ChangeOutline( distanceSq / ( 2 * maxDistanceForHighlight ) ) );
        }
    }

    IEnumerator ChangeOutline( float time )
    {
        //Debug.LogFormat( "{0} heard sound at {1}", gameObject.ToString(), time );
        yield return new WaitForSeconds( time );
        if( hightlightShading )
        {
            hightlightShading.enabled = true;
            //hightlightShading.OutlineMode = Outline.Mode.OutlineVisible;
        }

        yield return new WaitForSeconds( 1.0f );
        if( hightlightShading )
        {
            hightlightShading.enabled = false;
            //hightlightShading.OutlineMode = Outline.Mode.OutlineOff;
        }
    }


}
