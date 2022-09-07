using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LunchPad
{
    public class Pad_ClipButton : MonoBehaviour
    {
        public AudioClip ownClip = null;
        public Button button = null;

        private void Awake()
        {
            InitValue();
        }

        public void InitValue()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(() => ClipSet());
        }

        public void ClipSet()
        {
            if(Pad_Management.Instance.alreadyButton != null)
            {
                Pad_Management.Instance.alreadyButton.ownClip = ownClip;

                Pad_Management.Instance.EditReady();
            }
        }
    }
}