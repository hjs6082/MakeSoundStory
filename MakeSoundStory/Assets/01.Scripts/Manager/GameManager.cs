using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
#region ��¥
    public int month = 1;
    public int day = 1;
#endregion

#region ��
    public int playerMoney = 0;        //�÷��̾��� ������
    public int bankMoney = 500;

    public int playerDebt = 0;
#endregion

#region ����
    public int allCreativity; // ���� ��â��
    public int allAddictive; // ���� �ߵ���
    public int allMelodic; // ���� ��ε���?
    public int allPopularity; // ���� ���߼�
#endregion

    public int curBPM = 100;

    void Start()
    {
        //UIManagement.Instance.InitStaffPanels();
        //UIManager.instance?.GameStart();
        StartCoroutine(DayTimer()); 
    }

    void Update()
    {

    }

    public float[] GetStats()
    {
        float[] stats = {
            allAddictive,
            allCreativity,
            allMelodic,
            allPopularity
        };

        return stats;
    }

    IEnumerator DayTimer()
    {
        if (month != 12)
        {

            yield return new WaitForSeconds(120f);
            if (day != 31)
            {
                day++;
                UIManager.instance.CalendarSetting();
            }
            else
            {
                month++;
                day = 1;
                UIManager.instance.CalendarSetting();
            }
            StartCoroutine(DayTimer());

        }
    }
}
