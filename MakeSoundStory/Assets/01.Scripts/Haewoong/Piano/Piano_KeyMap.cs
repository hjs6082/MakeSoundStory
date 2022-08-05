using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class Piano_KeyMap : MonoBehaviour
    {
        private static Piano_KeyMap instance = null;
        public static Piano_KeyMap Instance => instance;

        [SerializeField]
        private Image[] pianoTile_Mapping_Images = new Image[8];
        public KeyCode[] pianoTile_Keys = new KeyCode[8];
        public List<KeyInfo> pianoTile_KeyInfo_List = new List<KeyInfo>();
        public Dictionary<KeyCode, Sprite> pianoTile_KeyImage_Dic = new Dictionary<KeyCode, Sprite>();

        public KeyCode curSelectedKey = KeyCode.None;
        private int curMapping = -1;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            InitValue();
        }

        private void Update()
        {

        }

        public void InitValue()
        {
            Sprite[] alphabets = Resources.LoadAll<Sprite>("Haewoong/Piano/Alphabets");
        }

        public void InitTile()
        {
            for(int i = 0; i < pianoTile_KeyInfo_List.Count; i++)
            {
                pianoTile_KeyInfo_List[i].InitSize();
            }
        }

        public void KeyMapping(int _idx)
        {
            if (curSelectedKey != KeyCode.None)
            {
                pianoTile_Keys[_idx] = curSelectedKey;
                pianoTile_Mapping_Images[_idx].sprite = pianoTile_KeyImage_Dic[curSelectedKey];
                curSelectedKey = KeyCode.None;
            }
        }
    }
}