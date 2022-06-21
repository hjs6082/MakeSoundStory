using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class Piano_KeyMap : MonoBehaviour
    {
        private readonly Color[] TILE_COLORS = {
            Color.red, Color.green, Color.blue, Color.yellow
        };

        private readonly KeyCode[] MAPPING_KEYS = {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
            KeyCode.Z
        };

        private List<Sprite> alphabet_Sprite_List = null;

        [SerializeField]
        private Button[] pianoTile_Mapping_Btns = new Button[8];
        public KeyCode[] pianoTile_Keys = new KeyCode[8];

        private bool isWaitting = false;
        private int curMapping = -1;

        private void Awake()
        {
             
        }

        private void Update()
        {
            
        }

        public void InitValue()
        {
            Sprite[] alphabets = Resources.LoadAll<Sprite>("Haewoong/Piano/Alphabets");

            alphabet_Sprite_List = new List<Sprite>(alphabets);

            
        }

        private void WaitMapping(int idx)
        {
            pianoTile_Mapping_Btns[idx].GetComponent<Image>().color = TILE_COLORS[idx];
            curMapping = idx;
            isWaitting = true;
        }

        private void InputKeyCode()
        {
            if(Input.anyKeyDown)
            {
                for(int i = 0; i < MAPPING_KEYS.Length; i++)
                {
                    if(Input.GetKeyDown(MAPPING_KEYS[i]))
                    {
                        Image pianoTile_Key_Img = pianoTile_Mapping_Btns[curMapping].GetComponentInChildren<Image>();

                        pianoTile_Keys[curMapping] = MAPPING_KEYS[i];
                        pianoTile_Key_Img.sprite = alphabet_Sprite_List[i];
                        curMapping = -1;
                        isWaitting = false;
                        return;
                    }
                }
            }
        }
    }
}