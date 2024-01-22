using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] clips;

     public AudioSource MusicSource;

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //if (!audioSource.isPlaying)
        if (!MusicSource.isPlaying)
        {
            MusicSource.clip = GetRandomClip();
            MusicSource.Play();
        }
    }
}
