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
    
        source.clip = LoadAudioClip();

    }

    public AudioClip LoadAudioClip()
    {
        return Resources.Load(clipName) as AudioClip;
    }

    public void PlayClip()
    {
        source.Play();
    }
}
