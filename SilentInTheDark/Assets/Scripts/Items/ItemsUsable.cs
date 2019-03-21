using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUsable : MonoBehaviour
{
    GameObject detectionPrefab;
    public GameObject outline;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        outline.SetActive(false);
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Floor" || col.gameObject.tag == "ItemS")
        {
            GameObject clone;
            clone = Instantiate(detectionPrefab, this.transform.position, transform.rotation);
            audioSource.PlayOneShot(audioClips[0]);
        }
    }
}
