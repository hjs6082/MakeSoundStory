using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Piano
{

    public class Piano_Stat : MonoBehaviour
    {
        [SerializeField] private int[] stat_Scores = null;
        [SerializeField] private int[] stat_Increase = null;

        [Header("Score UI 관련")]
        private const float UP_POS_Y = 720.0f;
        private const float DOWN_POS_Y = 125.0f;
        [SerializeField] private RectTransform start_Bar = null;
        [SerializeField] private Text[] stat_ScoreTexts = null;
        [SerializeField] private Image[] stat_Blocks = null;
        private bool isDown = true;

        private void Awake()
        {

        }

        public void InitValue()
        {
            stat_Scores = new int[4] { 0, 0, 0, 0 };
            stat_Increase = new int[4] { 1, 1, 1, 1 };

            if (GameManager.instance != null)
            {
                int staffCount = StaffManager.instance.pickWorkStaffList.Count;

                stat_Increase[0] = 1 + GameManager.instance.allPopularity / staffCount;
                stat_Increase[1] = 1 + GameManager.instance.allMelodic / staffCount;
                stat_Increase[2] = 1 + GameManager.instance.allAddictive / staffCount;
                stat_Increase[3] = 1 + GameManager.instance.allCreativity / staffCount;
            }

            start_Bar.anchoredPosition = new Vector2(0.0f, DOWN_POS_Y);
            isDown = true;
        }

        public void IncreaseScore(int _statIdx)
        {
            stat_Scores[_statIdx] += stat_Increase[_statIdx];
        }

        public void InitStatPanel()
        {
            float nextPos = (isDown) ? UP_POS_Y : DOWN_POS_Y;

            isDown = false;

            start_Bar.DOAnchorPosY(nextPos, 1.0f)
            .SetEase(Ease.InBounce)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                Piano_Management.Instance.P_Spawner.StartPiano();
            });
        }
    }
}
