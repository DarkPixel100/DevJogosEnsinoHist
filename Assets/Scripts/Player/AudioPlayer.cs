using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    public AudioClip jump;
    public AudioClip hit;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(string name)
    {
        switch (name)
        {
            case "jump":
                source.clip = jump;
                break;
            case "hit":
                source.clip = hit;
                break;
        }
        source.Play();
    }
}
