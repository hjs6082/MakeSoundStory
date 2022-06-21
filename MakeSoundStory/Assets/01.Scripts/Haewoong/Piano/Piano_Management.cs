using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class Piano_Management : MonoBehaviour
    {
        public static Piano_Management Instance => instance;
        private static Piano_Management instance = null;

        public static readonly Color[] STAT_COLORS = {
            Color.red, Color.green, Color.blue, Color.yellow
        };


        public Piano_Control P_Ctrl => p_Ctrl;
        public Piano_KeyMap P_KeyMap => p_KeyMap;
        public Piano_NoteSpawner P_Spawner => p_Spawner;
        public Piano_NoteChecker P_Checker => p_Checker;
        public Piano_Stat P_Stat => p_Stat;

        private Piano_Control p_Ctrl = null;
        private Piano_KeyMap p_KeyMap = null;
        private Piano_NoteSpawner p_Spawner = null;
        private Piano_NoteChecker p_Checker = null;
        private Piano_Stat p_Stat = null;

        public Action noteInput_Act = null;
        public Action<int> noteCheck_Act = null;

        /// <summary>
        /// 0 : 독창성, 1 : 대중성, 2 : 멜로디컬, 3 : 중독성
        /// </summary>
        public int[] total_Stats = new int[4];
        public List<GameObject> spawned_Note_List = null;
        public Transform guideLine_Parent = null;
        public GameObject tile_Prefab = null;
        public Transform tile_Parent = null;
        public bool bPlaying = false;

        private void Awake()
        {
            InitValue();
        }

        private void Update()
        {
            if(bPlaying) noteInput_Act?.Invoke();
        }

        private void InitSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Debug.LogError("다수의 매니지먼트");
            Destroy(this.gameObject);
        }

        private void InitValue()
        {
            InitSingleton();

            if (GameManager.instance != null)
            {
                total_Stats[0] = GameManager.instance.allCreativity; // 독창성
                total_Stats[1] = GameManager.instance.allPopularity; // 대중성
                total_Stats[2] = GameManager.instance.allMelodic;    // 멜로디컬
                total_Stats[3] = GameManager.instance.allAddictive;  // 중독성  
            }

            spawned_Note_List = new List<GameObject>();

            p_Ctrl = FindObjectOfType<Piano_Control>();
            p_KeyMap = FindObjectOfType<Piano_KeyMap>();
            p_Spawner = FindObjectOfType<Piano_NoteSpawner>();
            p_Checker = FindObjectOfType<Piano_NoteChecker>();
            p_Stat = FindObjectOfType<Piano_Stat>();

            p_Ctrl?.InitValue();
            p_KeyMap?.InitValue();
            p_Spawner?.InitValue();
            p_Checker?.InitValue();
            p_Stat?.InitValue();

            InitTile();
        }

        private void InitTile()
        {
            List<Sprite> tileImg_List = new List<Sprite>(Resources.LoadAll<Sprite>("Haewoong/Piano/Tiles"));
            List<Sprite> keyImg_List = new List<Sprite>();
            for(int i = 0; i < p_Ctrl.pianoTile_Keys.Length; i++)
            {
                Sprite img = Resources.Load<Sprite>($"Haewoong/Piano/Alphabets/Alphabet_{p_Ctrl.pianoTile_Keys[i].ToString()}");
                keyImg_List.Add(img);
            }

            for (int i = 0; i < 8; i++)
            {
                int idx = i;
                GameObject tile = Instantiate(tile_Prefab, tile_Parent);

                Piano_Tile pTile = tile.GetComponent<Piano_Tile>();

                pTile.tileImage.sprite = tileImg_List[i];
                pTile.keyImage.sprite = keyImg_List[i];
            }
        }
    }
}