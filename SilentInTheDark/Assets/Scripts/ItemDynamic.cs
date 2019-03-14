using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDynamic : MonoBehaviour
{
    AudioSource audioSource;
    public RippleState rippleEffect;
    float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.tag == "Floor" && timer >= 5)
        {
            audioSource.Play();
            if( rippleEffect )
                rippleEffect.RippleOrigin = transform.position;

            EventManager.Instance.NotifyObservers( gameObject.transform.position );
        }
    }
}
