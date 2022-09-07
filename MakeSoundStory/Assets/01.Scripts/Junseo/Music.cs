using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface MusicBase
{
    public void MakeStart();
    public void NoMake();
}

public class Music : MonoBehaviour, MusicBase
{
    [SerializeField]
    private Button makeStartButton;

    [SerializeField]
    private Button noMakeButton;

    void Start()
    {
        MakeStart();
        NoMake(); 
    }

    public void MakeStart()
    {
        makeStartButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            StaffManager.instance.RunNpc();
            UIManager.instance.staffTalkPanel.SetActive(false);
            UIManager.instance.MakeMusicStart();
        });
    }

    public void NoMake()
    {
        noMakeButton.onClick.AddListener(() =>
        {
            StaffManager.instance.RunNpc();
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false); 
        });
    }
}
