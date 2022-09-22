using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicListInfo : MonoBehaviour
{
    public MusicInfo music = default;

    [SerializeField] private Image music_Thumbnail = null;
    [SerializeField] private Text music_Title = null;
    [SerializeField] private Text music_Cost = null;
    [SerializeField] private Button music_Sell_Btn = null;

    private void Awake()
    {
        music_Sell_Btn.onClick.AddListener(() => {
            SellMusic();
        });
    }

    public void SetMusic(MusicInfo _music)
    {
        music = _music;

        music_Title.text = $"제목 : {_music.m_Name}";
        music_Cost.text = $"가격 : {_music.m_Cost}";
    }

    public void SellMusic()
    {
        GameManager.instance.playerMoney += (int)music.m_Cost;

        MusicManagement.instance.RemoveMusic(music);

        UIManagement.instance.GetStaffPanel<ShopPanel>().musicListInfo_List.Remove(this);
        UIManagement.instance.MoneyTextUpdate();

        Destroy(this.gameObject);
    }
}
