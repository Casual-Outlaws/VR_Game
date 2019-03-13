using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDynamic : MonoBehaviour
{
    AudioSource audioSource;
    public RippleState rippleEffect;

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
            audioSource.Play();
            if( rippleEffect )
                rippleEffect.RippleOrigin = transform.position;
        }
    }
}
