using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Control : MonoBehaviour
    {
        private Dictionary<KeyCode, int> pianoTile_Dic = null;
        public KeyCode[] pianoTile_Keys = new KeyCode[8]
        { 
            KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G,
            KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L 
        };

        private void Update()
        {
            InputStart();
        }

        public void InitValue()
        {
            pianoTile_Dic = new Dictionary<KeyCode, int>();
            for (int i = 0; i < pianoTile_Keys.Length; i++)
            {
                pianoTile_Dic.Add(pianoTile_Keys[i], i);
            }

            Piano_Management.Instance.noteInput_Act += () => InputPiano();
            Piano_Management.Instance.noteSound_Act += (x) => Sound_Management.Instance.PlayClip(x);
        }

        private void InputStart()
        {
            if(Input.GetKeyDown(KeyCode.Space) && !Piano_Management.Instance.bPlaying)
            {
                Piano_Management.Instance.MakeStart();
            }
        }

        private void InputPiano()
        {
            if (Input.anyKeyDown)
            {
                foreach (var tile in pianoTile_Dic)
                {
                    if (Input.GetKeyDown(tile.Key))
                    {
                        Piano_Management.Instance.noteCheck_Act?.Invoke(tile.Value);
                        Piano_Management.Instance.noteSound_Act?.Invoke(tile.Value);
                    }
                }
            }
        }
    }
}