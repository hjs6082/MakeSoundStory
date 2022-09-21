using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RandomMusic : MonoBehaviour
{

    public List<MusicInfo> music_List = null;

    private void Awake()
    {
        InitValue();
    }

    public void InitValue()
    {
        music_List = new List<MusicInfo>();

        //Music_List_Panel?.SetActive(false);
    }

    private void UpdateList(MusicInfo _music)
    {
        music_List.Add(_music);
        // 오브젝트 업데이트
    }

    
}