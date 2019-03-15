using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDynamic : MonoBehaviour
{
    AudioSource audioSource;
    public RippleState rippleEffect;
    bool detachedFromHand = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
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
        }
    }

    public void DetachedFromHand()
    {
        detachedFromHand = true;
    }
}
