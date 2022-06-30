using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Piano
{
    public class Piano_Management : MonoBehaviour
    {
        public static Piano_Management Instance => instance;
        private static Piano_Management instance = null;

        public static readonly Color[] STAT_COLORS = {
            Color.red, Color.green, Color.blue, Color.yellow
        };
        private const float PLAY_TIME = 20.0f;

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
        public List<Piano_Tile> tile_List = null;
        public bool bPlaying = false;
        public bool bSpawn = false;

        // 시작 관련
        private const float UP_POS_Y = 720.0f;
        private const float DOWN_POS_Y = 110.0f;
        private float curPlayTime = 0.0f;
        [SerializeField] private RectTransform start_Bar = null;
        public bool isDown = true;

        // 점수 관련
        public int combo = 0;
        public int totalScore = 0;
        [SerializeField] private TextMeshProUGUI comboTMP = null;
        [SerializeField] private TextMeshProUGUI totalScoreTMP = null;

        private void Awake()
        {
            InitValue();
        }

        private void Update()
        {
            if(bPlaying)
            {
                noteInput_Act?.Invoke();
            }

            if(bSpawn)
            {
                curPlayTime += Time.deltaTime;
                if (curPlayTime >= PLAY_TIME)
                {
                    curPlayTime -= curPlayTime;
                    bSpawn = false;

                    // TODO : 피아노 종료 시 실행
                    Debug.Log("끝");
                }
            }
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
            tile_List = new List<Piano_Tile>();

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
            InitStartPanel();

            curPlayTime = 0.0f;
            combo = 0;
            totalScore = 0;
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

                tile_List.Add(pTile);

                pTile.tileImage.sprite = tileImg_List[i];
                pTile.keyImage.sprite = keyImg_List[i];
            }
        }

        public void InitStartPanel()
        {
            start_Bar.anchoredPosition = new Vector2(0.0f, DOWN_POS_Y);
            isDown = true;

            start_Bar.DOAnchorPosY(140.0f, 0.75f)
            .SetLoops(-1, LoopType.Yoyo);
        }

        public void MakeStart()
        {
            float nextPos = (isDown) ? UP_POS_Y : DOWN_POS_Y;

            isDown = false;
            bPlaying = true;
            bSpawn = true;

            start_Bar.DOKill();
            start_Bar.DOAnchorPosY(nextPos, 1.0f)
            .SetEase(Ease.InBounce)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                P_Spawner.StartPiano();
            });
        }

        public void UpdateScore(int _increaseVal)
        {
            totalScore += _increaseVal;
            totalScoreTMP.text = totalScore.ToString();
        }

        public void UpdateCombo()
        {
            RectTransform comboTMP_Parent = comboTMP.transform.parent.GetComponent<RectTransform>();

            combo++;
            comboTMP.text = combo.ToString();

            comboTMP_Parent.DOComplete();
            comboTMP_Parent.DOShakeAnchorPos(0.25f, 20, 20);
        }
    }
}