using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int month = 1;
    public int day = 1;

    public int playerMoney = 0;        //�÷��̾��� ������
   
    public int allCreativity; // ���� ��â��
    public int allAddictive; // ���� �ߵ���
    public int allMelodic; // ���� ��ε���?
    public int allPopularity; // ���� ���߼�

    public int curBPM = 100;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("�̹� ���ӸŴ����� �����մϴ�.");
        }
        
        UIManager.instance?.GameStart();
        StartCoroutine(DayTimer()); 
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

    // Update is called once per frame
    void Update()
    {

    }

}
