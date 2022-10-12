using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;

public class Data
{
    public int day;
    public int playerMoney;
    public int playerDebt;
    public int staffIndex;
    public int officeStar;
    public Data(int day, int playerMoney, int playerDebt, int staffIndex, int officeStar)
    {
        this.day = day;
        this.playerMoney = playerMoney;
        this.playerDebt = playerDebt;
        this.staffIndex = staffIndex; 
        this.officeStar = officeStar;
    }
}


public class GameManager : MonoSingleton<GameManager>
{
    public int day = 0;

    public int playerMoney = 0;        //占시뤄옙占싱억옙占쏙옙 占쏙옙占쏙옙占쏙옙

    public int bankMoney = 500;

    public int playerNote = 0;

    public int officeStar = 0;

    public int playerDebt = 0;

    public int allCreativity; // 占쏙옙占쏙옙 占쏙옙창占쏙옙
    public int allAddictive; // 占쏙옙占쏙옙 占쌩듸옙占쏙옙
    public int allMelodic; // 占쏙옙占쏙옙 占쏙옙琯占쏙옙占?
    public int allPopularity; // 占쏙옙占쏙옙 占쏙옙占쌩쇽옙

    public int curBPM = 100;

    private float time = 0.0f;
    private bool isTime = false;

    public Text dayText;
    public Text timeText;


    public GameObject resultPanel;
    public Text dayResultText;
    public Text goldResultText;
    public Text debtResultText;
    public Text staffResultText;
    public Text starResultText;
    public Button nextDayButton;

    public AudioSource clickSource = null;

    void Start()
    {
        //UIManagement.Instance.InitStaffPanels();
        //UIManager.instance?.GameStart();
        StartTime();

        day = 0;
        playerMoney = 1000;        //占시뤄옙占싱억옙占쏙옙 占쏙옙占쏙옙占쏙옙
        bankMoney = 500;
        playerNote = 0;
        officeStar = 0;
        playerDebt = 0;

        clickSource = GetComponent<AudioSource>();
        clickSource.clip = Resources.Load("SoundClips/Click") as AudioClip;

        nextDayButton.onClick.AddListener(() =>
        {
            NextDay();
        });
        NextDay();
    }

    void Update()
    {
        if (isTime)
        {
            time += 5 * Time.deltaTime;
            TextSetting();
            if (time >= 540f)
            {
                ShowResult();
                //NextDay();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            clickSource?.Stop();
            clickSource?.Play();
        }
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
    

    #region Time

    void TextSetting()
    {
        dayText.text = day + " 일차";
        int hour = Mathf.FloorToInt(time / 60.0f);
        int minutes = Mathf.FloorToInt(time - hour * 60);
        timeText.text = string.Format("{0:00}:{1:00}", hour + 9, minutes);
    }

    public void StartTime()
    {
        isTime = true;
    }

    public void StopTime()
    {
        isTime = false;
    }

    public void ResetTimer()
    {
        time = 0.0f;
    }

    public void ShowResult()
    {
        StopTime();
        resultPanel.SetActive(true);
        UIManagement.instance.isPanelOn = true;
        dayResultText.DOText(day + "일차 결과", 1f).OnComplete(() =>
        {
            goldResultText.DOText(playerMoney + "G", 1f).OnComplete(() =>
            {
                staffResultText.DOText(StaffManager.instance.workStaffList.Count + "명", 1f).OnComplete(() =>
                {
                    debtResultText.DOText(playerDebt + "G", 1f).OnComplete(() =>
                    {
                        starResultText.DOText(officeStar + "성", 1f).OnComplete(() =>
                        {
                        });
                            nextDayButton.gameObject.SetActive(true); 
                    });
                });
            });
        });
    }

    public void NextDay()
    {
        ResetTimer();

        RemoveText(dayResultText);
        RemoveText(goldResultText);
        //RemoveText(starResultText);
        RemoveText(debtResultText);
        RemoveText(staffResultText);
        resultPanel.SetActive(false);

        UIManagement.instance.isPanelOn = false;

        Data data = new Data(day, playerMoney, playerDebt, StaffManager.instance.workStaffList.Count, officeStar);
        Save(data);

        day++;
        StartTime();
    }

    public void RemoveText(Text text)
    {
        text.text = "";
    }
    #endregion

    #region SaveandLoad
    void Save(Data data)
    {
        JsonData jsondata = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/Data/DayData.json", jsondata.ToString());
        Debug.Log(jsondata);
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Data/DayData.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }
    #endregion

}
