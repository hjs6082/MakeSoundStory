using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePanel : MonoBehaviour
{
    private const float MAX_HEIGHT = 300.0f;
    private const float STAT_INCREASE_OFFSET = 50.0f;

    private MusicPanel musicPanel = null;

    private bool isPlaying = false;

    private float maxTime   = 10.0f;
    private float curTime   = 0.0f;
    public  Image timeGuage = null;

    private int     curStat     = 0;
    public  float[] resultStats = null;
    public  float[] statIncreases = null;
    public  Image[] statGuages = null;
    public  Image   spaceBarImage = null;

    [SerializeField] private Button nextButton = null;
    private LoadClip bgm = null;

    private void Update()
    {
        if(isPlaying)
        {
            InputGame();
        }
    }

    private void FixedUpdate()
    {
        if(isPlaying)
        {
            Timer(Time.fixedDeltaTime);
        }
    }

    public void InitValue()
    {
        musicPanel = UIManagement.instance.GetStaffPanel<MusicPanel>();
        bgm = FindObjectOfType<LoadClip>();

        isPlaying = false;

        curTime = 0.0f;
        curStat = 0;

        resultStats   = new float[4]{ 0, 0, 0, 0 };
        statIncreases = new float[4]{ 0, 0, 0, 0 };

        nextButton.onClick.AddListener(() => {
            musicPanel.MusicOut();
            bgm.source.Play();
            OffPanel();
        });
        nextButton.gameObject.SetActive(false);

        OffPanel();
    }

    public void InputGame()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Tab();

            spaceBarImage.color = Color.gray;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            spaceBarImage.color = Color.white;
        }
    }

    public void ResetGame()
    {
        Vector2 size;

        for(int i = 0; i < resultStats.Length; i++)
        {
            resultStats[i] = 0.0f;

            size = statGuages[i].rectTransform.sizeDelta;
            size.y = 0.0f;

            statGuages[i].rectTransform.sizeDelta = size;
        }

        for(int i = 0; i < statIncreases.Length; i++)
        {
            float allStat = 0.0f;
            for(int j = 0; j < musicPanel.selected_Staff_List.Count; j++)
            {
                float stat = 0;
                switch(i)
                {
                    case 0: stat = musicPanel.selected_Staff_List[j].Creativity; break;
                    case 1: stat = musicPanel.selected_Staff_List[j].Addictive;  break;
                    case 2: stat = musicPanel.selected_Staff_List[j].Melodic;    break;
                    case 3: stat = musicPanel.selected_Staff_List[j].Popularity; break;
                }

                allStat += stat;
            }

            // 스탯 증가치 정하기
            statIncreases[i] = allStat / STAT_INCREASE_OFFSET;
        }

        curTime = 0.0f;
        curStat = 0;
    }

    public void OnPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void OffPanel()
    {
        nextButton.gameObject.SetActive(false);

        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        OnPanel();
        ResetGame();

        bgm.source.Pause();

        isPlaying = true;
    }

    public void EndGame()
    {
        isPlaying = false;

        nextButton.gameObject.SetActive(true);
        //OffPanel();
    }

    public void Tab()
    {
        int statIdx = curStat % 4;

        float statIncrease = statIncreases[statIdx] * UnityEngine.Random.Range(1.0f, 2.0f);
        resultStats[statIdx] = Mathf.Clamp(resultStats[statIdx] + statIncrease, 0, statIncreases[statIdx] * STAT_INCREASE_OFFSET);

        Vector2 size = statGuages[statIdx].rectTransform.sizeDelta;
        size.y = resultStats[statIdx] / (statIncreases[statIdx] * STAT_INCREASE_OFFSET) * 350.0f;
        statGuages[statIdx].rectTransform.sizeDelta = size;

        musicPanel.PlayDrum(statIdx);

        curStat = UnityEngine.Random.Range(0, 4);
    }

    public void Timer(float _delta)
    {
        curTime += _delta;

        Vector2 timeSize = timeGuage.rectTransform.sizeDelta;
        timeSize.x = (maxTime - curTime) / maxTime * 650.0f;
        timeGuage.rectTransform.sizeDelta = timeSize;

        if(curTime >= maxTime)
        {
            EndGame();
        }
    }
}
