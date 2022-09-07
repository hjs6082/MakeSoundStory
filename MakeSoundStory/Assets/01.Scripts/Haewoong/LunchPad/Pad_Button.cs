using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LunchPad
{
    public class Pad_Button : MonoBehaviour
    {
        public int ownIdx = 0;
        public AudioClip ownClip = null;
        public KeyCode ownKey = default;
        public AudioSource ownSource = null;
        public Button ownButton = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            ownSource = gameObject.AddComponent<AudioSource>();
            ownButton = GetComponent<Button>();

            ownButton.onClick.AddListener(() => SetClip());
        }

        public void SetClip()
        {
            if(Pad_Management.Instance.isEditMode)
            {
                Pad_Management.Instance.EditReady(ownIdx);
            }
            else
            {
                Play();
            }
        }

        public void Record()
        {
            Pad_Management.Instance.pad_Recorder.Record(ownIdx);
        }

        public void Play()
        {
            if (ownClip != null)
            {
                ownSource.PlayOneShot(ownClip);

                Record();
            }
        }
    }
}