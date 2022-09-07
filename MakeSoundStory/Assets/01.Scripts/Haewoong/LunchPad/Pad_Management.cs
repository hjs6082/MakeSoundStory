using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunchPad
{
    public class Pad_Management : MonoBehaviour
    {
#region Singleton
        public static Pad_Management Instance => instance;
        private static Pad_Management instance = null;

        private void InitSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Debug.LogWarningFormat("다수의 {0} 실행 중", this.name);
        }
#endregion

#region LunchPad Class
        public Pad_Spawner    pad_Spawner    = null;
        public Pad_Controller pad_Controller = null;
        public Pad_Recorder   pad_Recorder   = null;
#endregion

#region Clip Btn
        public List<AudioClip> allClips = null;
        public List<Pad_ClipButton> pad_ClipButton_List = null;

        public GameObject clipBtn_Prefab = null;
        public Transform clipBtn_Parent = null;
        public Transform clipListTrm = null;
#endregion

#region Clip Set
        public bool isEditMode = false;
        public Pad_Button alreadyButton = null;
#endregion

#region Play Lunch Pad
        public float bpmSpeed = 0.0f;
#endregion

        private void Awake()
        {
            InitSingleton();
            InitValue();
        }

        private void InitValue()
        {
            if(GameManager.instance != null) { bpmSpeed = (float)GameManager.instance.curBPM / 60.0f; }
            else { bpmSpeed = 0.2f; }

            pad_Spawner = FindObjectOfType<Pad_Spawner>();
            pad_Controller = FindObjectOfType<Pad_Controller>();
            pad_Recorder = FindObjectOfType<Pad_Recorder>();

            pad_Spawner.InitValue();
            pad_Controller.InitValue();

            //allClips = new List<AudioClip>();

            for(int i = 0; i < allClips.Count; i++)
            {
                GameObject clipBtn = Instantiate(clipBtn_Prefab, clipBtn_Parent);
                Pad_ClipButton padClipBtn = clipBtn.GetComponent<Pad_ClipButton>();

                padClipBtn.ownClip = allClips[i];
                pad_ClipButton_List.Add(padClipBtn);
            }

            clipListTrm.gameObject.SetActive(false);
        }

        public void ChangeEditMode()
        {
            print("EditMode");
            isEditMode = !isEditMode;
            clipListTrm.gameObject.SetActive(isEditMode);
        }

        public void EditReady(int _idx = -1)
        {
            if(_idx != -1)
            {
                for(int i = 0; i < pad_Spawner.pad_Btn_List.Count; i++)
                {
                    pad_Spawner.pad_Btn_List[i].ownButton.interactable = false;
                }

                alreadyButton = pad_Spawner.pad_Btn_List[_idx];
                alreadyButton.ownButton.interactable = true;
            }
            else
            {
                for(int i = 0; i < pad_Spawner.pad_Btn_List.Count; i++)
                {
                    pad_Spawner.pad_Btn_List[i].ownButton.interactable = true;
                }

                alreadyButton = null;
            }
        }
    }
}
