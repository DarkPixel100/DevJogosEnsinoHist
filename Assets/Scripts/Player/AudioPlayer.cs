using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    public AudioClip jump;
    public AudioClip hit;

    public AudioClip death;

    public AudioClip collectHealth;
    public AudioClip collectObjective;
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
            case "death":
                source.clip = death;
                break;
            case "collectHealth":
                source.clip = collectHealth;
                break;
            case "collectObjective":
                source.clip = collectObjective;
                break;
        }
        source.Play();
    }
}
