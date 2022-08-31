using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LunchPad
{
    public class Pad_Controller : MonoBehaviour
    {
        [SerializeField]
        private Button recordButton = null;
        private List<Pad_Button> pad_Btn_List = null;

        public void InitValue()
        {
            recordButton.onClick.RemoveAllListeners();
            recordButton.onClick.AddListener(() => { Recording(); });

            pad_Btn_List = new List<Pad_Button>(Pad_Management.Instance.pad_Spawner.pad_Btn_List);
        }

        public void Recording()
        {
            Pad_Management.Instance.pad_Recorder.isRec = true;
        }

        public void InputLunchPad()
        {
            if (Input.anyKeyDown)
            {
                for (int i = 0; i < pad_Btn_List.Count; i++)
                {
                    if(Input.GetKeyDown(pad_Btn_List[i].ownKey))
                    {
                        pad_Btn_List[i].Play();
                    }
                }
            }
        }
    }
}