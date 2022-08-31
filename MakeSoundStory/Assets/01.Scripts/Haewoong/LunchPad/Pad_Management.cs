using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunchPad
{
    public class Pad_Management : MonoBehaviour
    {
#region ½Ì±ÛÅæ
        public static Pad_Management Instance => instance;
        private static Pad_Management instance = null;

        private void InitSingleton()
        {
            if (instance = null)
            {
                instance = this;
                return;
            }

            Debug.LogWarningFormat("´Ù¼öÀÇ {0} ½ÇÇà Áß", this.name);
        }
#endregion
    
        public Pad_Spawner    pad_Spawner    = null;
        public Pad_Controller pad_Controller = null;
        public Pad_Recorder   pad_Recorder   = null;

        public List<AudioClip> allClips = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            pad_Spawner = FindObjectOfType<Pad_Spawner>();
            pad_Controller = FindObjectOfType<Pad_Controller>();
            pad_Recorder = FindObjectOfType<Pad_Recorder>();

            pad_Spawner.InitValue();
            pad_Controller.InitValue();

            allClips = new List<AudioClip>();
        }
    }
}
