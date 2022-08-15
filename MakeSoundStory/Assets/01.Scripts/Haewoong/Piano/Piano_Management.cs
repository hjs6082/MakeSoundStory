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
        public  static Piano_Management Instance => instance;
        private static Piano_Management instance = null;

        private const float DEFAULT_PLAY_TIME = 60.0f; // 기본 게임 플레이 시간

#region 클래스
        [Header("피아노 클래스")]
        public Piano_Control     p_Ctrl    = null;
        public Piano_KeyMap      p_KeyMap  = null;
        public Piano_NoteSpawner p_Spawner = null;
        public Piano_Stat        p_Stat    = null;   
#endregion

#region Action
        [Header("피아노 Action 변수")]
        public Action      noteInput_Act = null;
        public Action<int> noteCheck_Act = null;
        public Action<int> noteSound_Act = null;
#endregion

#region bool
        [Header("BOOL 변수들")]
        public bool bPlaying = false; // 플레이 중인지
#endregion

#region UI 변수
        [Header("피아노 UI 관련 변수")]
        public Text            startText        = null;
        public Text            secondText       = null;
        public Image           secondGuage      = null;
        public GameObject      tile_Prefab      = null;
        public Transform       tile_Parent      = null;
        public Transform       guideLine_Parent = null;
#endregion

        private float curPlayTime = 0.0f;

        private void Awake()
        {
            InitSingleton();
            InitialValue();
        }

        private void Update()
        {
            if(bPlaying)
            {
                noteInput_Act?.Invoke();

                curPlayTime += Time.deltaTime;
                secondText.text = ((int)(DEFAULT_PLAY_TIME - curPlayTime)).ToString();
                secondGuage.fillAmount = curPlayTime / DEFAULT_PLAY_TIME;
                if (curPlayTime >= DEFAULT_PLAY_TIME)
                {
                    curPlayTime -= curPlayTime;
                    bPlaying = false;
                    print("끝");
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

            Destroy(this.gameObject);
        }
        private void InitialValue()
        {
            InitPiano();
            InitTile();

            startText.fontSize = 130;
            startText.text = "SPACE TO START";
            curPlayTime = 0.0f;
        }
        private void InitPiano()
        {
            p_Ctrl = FindObjectOfType<Piano_Control>();
            p_KeyMap = FindObjectOfType<Piano_KeyMap>();
            p_Spawner = FindObjectOfType<Piano_NoteSpawner>();
            p_Stat = FindObjectOfType<Piano_Stat>();

            p_Ctrl?.InitValue();
            p_KeyMap?.InitValue();
            p_Spawner?.InitValue();
            p_Stat?.InitValue();
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

        public void MakeStart()
        {
            // TODO : 메트로놈 소리 3번 후 시작
            StartCoroutine(Metronome(() => 
            {
                print("시작");
                bPlaying = true;
            }));
        }

        private IEnumerator Metronome(Action _callBack)
        {
            var delay = new WaitForSeconds(1.0f);

            for(int i = 0; i < 4; i++)
            {
                // 사운드
                Sound_Management.Instance.PlayMetronome();

                startText.fontSize = (i < 3) ? 300 : 200;
                startText.text = (i < 3) ? (3 - i).ToString() : "START";

                yield return delay;
            }

            startText.gameObject.SetActive(false);

            _callBack?.Invoke();
        }
    }
}