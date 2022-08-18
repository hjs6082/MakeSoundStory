using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPM_Class
{
    public double[] MaxBPM = new double[10]
    {
        130, 80, 115, 160, 100,
        70, 140, 140, 125, 200
    };

    public double[] MinBPM = new double[10]
    {
        100, 60, 100, 100, 80,
        50, 60, 110, 120, 140
    };
}

public class BPM_Management : MonoBehaviour
{
    public struct GenreBPM
    {
        private double maxBPM;
        private double minBPM;

        public double MaxBPM
        { 
            get { return maxBPM; } 
            set { maxBPM = value; } 
        }

        public double MinBPM
        { 
            get { return minBPM; } 
            set { minBPM = value; } 
        }
    }

    public static BPM_Management Instance = null;

    private BPM_Class      bpm_Class     = null;
    private Dictionary<int, GenreBPM> genreBPM_Dic = null;
    public GenreBPM curGenre = default;

    public Text BPM_Text = null;
    private StringBuilder sb = new StringBuilder();
    public Slider BPM_Slider = null;
    public Button BPM_Play_Button = null;
    private Coroutine BPM_Routine = null;

    public int curBPM = 0;
    public bool isSetting = false;
    public bool isPlaying = false;

    private void Awake()
    {
        InitValue();

        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this.gameObject);
    }

    public void InitValue()
    {
        bpm_Class = new BPM_Class();
        genreBPM_Dic = new Dictionary<int, GenreBPM>();
        GenreBPM genreBPM = new GenreBPM();

        for(int i = 0; i < 10; i++)
        {
            genreBPM.MaxBPM = bpm_Class.MaxBPM[i];
            genreBPM.MinBPM = bpm_Class.MinBPM[i];

            genreBPM_Dic.Add(i + 1, genreBPM);
        }

        BPM_Slider.onValueChanged.AddListener((x) => 
        {
            SettingBPM(x);
        });

        BPM_Play_Button.onClick.AddListener(() => 
        {  
            BPM_State();
        });
    }

    private void SettingBPM(float _value)
    {
        if(isPlaying) BPM_State();

        curBPM = (int)(curGenre.MinBPM + (curGenre.MaxBPM - curGenre.MinBPM) * _value);
        GameManager.instance.curBPM = curBPM;
        UpdateText();
    }

    private void UpdateText()
    {
        sb.Clear();
        sb.AppendFormat("{0} / {1} ~ {2}", curBPM, curGenre.MinBPM, curGenre.MaxBPM);

        BPM_Text.text = sb.ToString();
    }

    public void Set_BPM_Dic(int _genreIndex)
    {
        curGenre = genreBPM_Dic[_genreIndex];
        SettingBPM(BPM_Slider.value);
    }

    public void BPM_State()
    {
        isPlaying = !isPlaying;

        BPM_Play_Button.GetComponentInChildren<Text>().text = (isPlaying) ? "II" : "¢º";

        if(BPM_Routine != null)
        {
            StopCoroutine(BPM_Routine);
        }
        BPM_Routine = StartCoroutine(Play_BPM(curBPM));
    }

    private IEnumerator Play_BPM(int _bpm)
    {
        float delayTime = 60.0f / (float)_bpm;
        var delay = new WaitForSeconds(delayTime);

        while(isPlaying)
        {
            Sound_Management.Instance.PlayMetronome();

            yield return delay;
        }
    }
}
