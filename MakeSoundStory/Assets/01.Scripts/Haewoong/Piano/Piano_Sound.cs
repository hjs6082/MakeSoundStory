using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{

    public class Piano_Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSources = null;
        [SerializeField] private AudioClip[] curClips = null;
        [SerializeField] private AudioClip[] pianoClips = null;
        [SerializeField] private AudioClip[] drumClips = null;

        public void InitValue()
        {
            InitSource();
            ClipsToPiano();
            Piano_Management.Instance.noteSound_Act += (x) => PlayClip(x);
        }

        private void InitSource()
        {
            audioSources = GetComponentsInChildren<AudioSource>();

            for(int i = 0; i < audioSources.Length; i++)
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

            for(int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].clip = curClips[i];
            }
        }

        public void ClipsToPiano()
        {
            ChangeClip(pianoClips);
        }

        public void ClipsToDrum()
        {
            ChangeClip(drumClips);
        }

        public void PlayClip(int _idx)
        {
            audioSources[_idx].Stop();
            audioSources[_idx].Play();
        }
    }
}