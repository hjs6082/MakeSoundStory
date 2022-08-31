using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunchPad
{
    public class Pad_Button : MonoBehaviour
    {
        public int ownIdx = 0;
        public AudioClip ownClip = null;
        public KeyCode ownKey = default;
        public AudioSource ownSource = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            ownSource = gameObject.AddComponent<AudioSource>();
        }

        public void SetClip(AudioClip _clip)
        {
            ownClip = _clip;
        }

        public void Record()
        {
            Pad_Management.Instance.pad_Recorder.Record(ownIdx);
        }
    
        public void Play()
        {
            ownSource.PlayOneShot(ownClip);
            Record();
        }
    }
}