using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct MusicInfo
{
    public string name;
    public float[] stats;

    public MusicInfo(string _name, float[] _stats)
    {
        name = _name;
        stats = _stats;
    }
}

public class CompleteMusicPanel : MonoBehaviour
{
    private readonly string[] stat_Names = { "독창성", "중독성", "멜로디컬", "대중성" };
    private readonly Color[] stat_Colors = { Color.red, Color.yellow, Color.blue, Color.green };

    [SerializeField] private Text[] stat_Texts = null;
    [SerializeField] private Text[] stat_Values = null;
    [SerializeField] private InputField title_Field = null;
    [SerializeField] private Button make_Button = null;
    [SerializeField] private MusicInfo makedMusic = default;

    private float[] stats = null;

    public void InitValue()
    {
        title_Field = this.transform.GetComponentInChildren<InputField>();
        make_Button = this.transform.GetComponentInChildren<Button>();

        make_Button.onClick.AddListener(() => {
            SaveMusic();
        });

        for(int i = 0; i < stat_Texts.Length - 1; i++)
        {
            stat_Texts[i].text = stat_Names[i];
        }

        OffPanel();
    }

    public void OnPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void OffPanel()
    {
        this.gameObject.SetActive(false);
    }   

    public void MakeComplete(float[] _randStats, float[] _allStats)
    {
        for(int i = 0; i < stat_Texts.Length; i++)
        {
            Image statImage = stat_Texts[i].GetComponentInChildren<Image>();
            statImage.fillAmount = _randStats[i] / _allStats[i];
            statImage.color = stat_Colors[i];

            Text statValue = stat_Values[i];
            statValue.text = $"{_randStats[i]:F0} / {_allStats[i]}";
        }

        stats = _randStats;

        OnPanel();
    }

    public void SaveMusic()
    {
        string musicName = title_Field.text;
        makedMusic = new MusicInfo(musicName, stats);

        SaveSystem.Save<MusicInfo>(makedMusic, musicName + "_MUSIC");
    }
}