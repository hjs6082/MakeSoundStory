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

        [Header("Score UI ����")]
        [SerializeField] private Text[] stat_ScoreTexts = null;
        [SerializeField] private Image[] stat_Blocks = null;

        public void InitValue()
        {
            stat_Scores = new int[4] { 0, 0, 0, 0 };
            stat_Increase = new int[4] { 1, 1, 1, 1 };

            if (GameManager.instance != null)
            {
                int staffCount = StaffManager.instance.pickWorkStaffList.Count;
                
                if(staffCount > 0)
                {
                    stat_Increase[0] = 1 + GameManager.instance.allPopularity / staffCount;
                    stat_Increase[1] = 1 + GameManager.instance.allMelodic / staffCount;
                    stat_Increase[2] = 1 + GameManager.instance.allAddictive / staffCount;
                    stat_Increase[3] = 1 + GameManager.instance.allCreativity / staffCount;
                }
            }
        }

        public void IncreaseScore(int _statIdx)
        {
            stat_Scores[_statIdx] += stat_Increase[_statIdx];
        }
    }
}
