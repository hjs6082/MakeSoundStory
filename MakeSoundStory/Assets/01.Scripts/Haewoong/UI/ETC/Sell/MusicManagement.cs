using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    public readonly string MUSIC_NAMES_SAVE = "MusicNameSaves";

    public static MusicManagement instance = null;

    private List<MusicInfo> musicList = null;

    private void Awake()
    {
        InitValue();
    }

    public void InitValue()
    {
        if(instance == null)
        {
            instance = this;
        }

        musicList = new List<MusicInfo>();
    }

    public void AddMusic(MusicInfo _music)
    {
        musicList.Add(_music);
    }

    public void RemoveMusic(MusicInfo _music)
    {
        musicList.Remove(_music);
    }

    // public void LoadAllMusic()
    // {
    //     // string[] musicNames = SaveSystem.Load<MusicNames>(MUSIC_NAMES_SAVE).music_Names;

    //     if(musicNames != null)
    //     {
    //         for (int i = 0; i < musicNames.Length; i++)
    //         {
    //             MusicInfo music = SaveSystem.Load<MusicInfo>(musicNames[i] + "_Music");
    //             musicList.Add(music);
    //         }

    //     }
    // }

    public MusicInfo[] GetMusicList()
    {
        // LoadAllMusic();

        return musicList.ToArray();
    }
}
