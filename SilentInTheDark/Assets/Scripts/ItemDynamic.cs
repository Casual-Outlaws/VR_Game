using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDynamic : MonoBehaviour
{
    AudioSource audioSource;
    public RippleState rippleEffect;
    bool detachedFromHand = false;
    float timer;
    [SerializeField] GameObject detectionPrefab;

    void Start()
    {
        timer = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
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
}
