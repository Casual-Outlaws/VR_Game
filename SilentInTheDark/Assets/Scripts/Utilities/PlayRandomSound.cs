using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;
    // Start is called before the first frame update

    private void Awake()
    {
        this.source.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlaySound();
    }

    void PlaySound()
    {
        source.Stop();
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        print(source.clip);
    }
}
