using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadClip : MonoBehaviour
{
    public string clipName = "";
    public AudioSource source = null;

    private void Awake()
    {
        source = GetComponent<AudioSource>();        

        source.volume = 0.5f;
        source.clip = LoadAudioClip();
        PlayClip();
    }

    public AudioClip LoadAudioClip()
    {
        return Resources.Load($"SoundClips/{clipName}") as AudioClip;
    }

    public void PlayClip()
    {
        source.Play();
    }
}
