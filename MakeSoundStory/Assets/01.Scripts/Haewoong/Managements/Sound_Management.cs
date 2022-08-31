using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Management : MonoBehaviour
{
    public static Sound_Management Instance = null;

    public AudioClip[] curClips = null;
    
    [SerializeField] private AudioSource[] audioSources = null;
    [SerializeField] private AudioClip[] pianoClips = null;
    [SerializeField] private AudioClip metronomeClip = null;

    private void Awake()
    {
        InitValue();
    }

    public void InitValue()
    {
        InitSource();
        ClipsToPiano();

        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this.gameObject);
    }

    private void InitSource()
    {
        audioSources = GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].playOnAwake = false;
            audioSources[i].loop = false;
            audioSources[i].mute = false;
            audioSources[i].volume = 0.2f;
        }
    }

    private void ChangeClip(AudioClip[] _clips)
    {
        curClips = _clips;

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].clip = curClips[i];
        }
    }

    private void ClipsToPiano()
    {
        ChangeClip(pianoClips);
    }

    public void PlayClip(int _idx)
    {
        audioSources[_idx].Stop();
        audioSources[_idx].Play();
    }

    public void PlayMetronome()
    {
        audioSources[0].PlayOneShot(metronomeClip);
    }
}