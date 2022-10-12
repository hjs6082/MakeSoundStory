using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : StaffPanel
{
    public override Action init { get; set; }= null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;

    [SerializeField] private ScrollRect musicScrollRect = null;
    [SerializeField] private RectTransform musicListParent = null;
    [SerializeField] private GameObject musicListPrefab = null;

    public List<MusicListInfo> musicListInfo_List = null;

    protected override void Awake()
    {
        exitButton = exitBtn;
        base.Awake();
    }

    protected override void Update()
    {
        
    }

    protected override void InitValue()
    {
        musicListInfo_List = new List<MusicListInfo>();

        TextUpdate();
        OffPanel();

        base.InitValue(); 
    }
        
    protected override void TextUpdate()
    {
        
    }
    
    public override void OnPanel()
    {
        MusicInfo[] musics = MusicManagement.instance.GetMusicList();

        if (musics.Length > 0)
        {
            musicScrollRect.gameObject.SetActive(true);

            for (int i = 0; i < musics.Length; i++)
            {
                GameObject music = Instantiate<GameObject>(musicListPrefab, musicListParent);
                MusicListInfo musicInfo = music.GetComponent<MusicListInfo>();

                musicListInfo_List.Add(musicInfo);
                musicInfo.SetMusic(musics[i]);
            }
        }
        else
        {
            musicScrollRect.gameObject.SetActive(false);
        }

        TextUpdate();
        base.OnPanel();
    }

    public override void OffPanel()
    {
        // TODO : 패널 초기화 스크립트 작성
        if (musicListInfo_List != null)
        {
            for (int i = 0; i < musicListInfo_List.Count; i++)
            {
                Destroy(musicListInfo_List[i].gameObject);
            }
            
            musicListInfo_List.Clear();
        }


        base.OffPanel();
    }
}
