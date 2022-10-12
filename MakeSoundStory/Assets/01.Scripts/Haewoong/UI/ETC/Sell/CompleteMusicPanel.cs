using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [System.Serializable]
// public struct MusicNames
// {
//     public string[] music_Names;

//     public MusicNames(string[] _musicNames)
//     {
//         music_Names = _musicNames;   
//     }
// }

[System.Serializable]
public struct MusicInfo
{
    public string m_Name;
    public float[] m_Stats;
    public float m_Cost;
    public bool m_IsSoldOut;

    public MusicInfo(string _name, float[] _stats, float _cost, bool _isSoldOut)
    {
        m_Name = _name;
        m_Stats = _stats;
        m_Cost = _cost;
        m_IsSoldOut = _isSoldOut;
    }
}

public class CompleteMusicPanel : MonoBehaviour
{
    private readonly string[] stat_Names = { "독창성", "중독성", "멜로디컬", "대중성" };

    [SerializeField] private Text[] stat_Texts = null;
    [SerializeField] private Text[] stat_Values = null;
    [SerializeField] private InputField title_Field = null;
    [SerializeField] private Button make_Button = null;
    [SerializeField] private MusicInfo makedMusic = default;
    [SerializeField] private List<string> musicName_List = null;

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
        Vector2 sizeOffset = new Vector2(0, 30);

        for(int i = 0; i < stat_Texts.Length; i++)
        {
            Image statImage = stat_Texts[i].GetComponentInChildren<Image>();
            sizeOffset.x = (_randStats[i] / _allStats[i]) * 300;
            statImage.rectTransform.sizeDelta = sizeOffset;

            Text statValue = stat_Values[i];
            statValue.text = $"{_randStats[i]:F0} / {_allStats[i]}";
        }

        stats = _randStats;

        OnPanel();
    }

    public void SaveMusic()
    {
        // string [] loadMusicNames = SaveSystem.Load<MusicNames>(MusicManagement.instance.MUSIC_NAMES_SAVE).music_Names;
        // musicName_List = new List<string>(loadMusicNames);

        string musicName = title_Field.text;
        float musicCost = 0;

        for(int i = 0; i < stats.Length; i++)
        {
            musicCost += stats[i];
        }

        musicCost = Mathf.Round(musicCost) * 10;
        makedMusic = new MusicInfo(musicName, stats, musicCost, false);

        // musicName_List.Add(musicName);
        // MusicNames musicNames = new MusicNames(musicName_List.ToArray());
        // SaveSystem.Save<MusicNames>(musicNames, MusicManagement.instance.MUSIC_NAMES_SAVE);

        MusicManagement.instance.AddMusic(makedMusic);
        SaveSystem.Save<MusicInfo>(makedMusic, musicName + "_MUSIC");

        title_Field.text = string.Empty;
        
        UIManagement.instance.GetStaffPanel<MusicPanel>().OffPanel();
    }
}