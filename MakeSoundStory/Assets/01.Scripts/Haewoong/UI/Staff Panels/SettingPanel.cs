using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : StaffPanel
{
    public override Action init { get; set; }= null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;

    [SerializeField] private GameObject quitAskPanel = null;
    private Action<bool> quitAct = null;
    private bool isApplicationQuit = false;

    [SerializeField] private Button quitButton = null;
    [SerializeField] private Button onQuitButton = null;
    [SerializeField] private Button onCancelButton = null;

#region 추상 메소드
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
        quitAct += (isOn) => {
            quitAskPanel.SetActive(isOn);
        };
        quitAct?.Invoke(false);

        Application.wantsToQuit += () => ApplicationQuit();

        quitButton.onClick.AddListener(() => { ApplicationQuit(); });
        onQuitButton.onClick.AddListener(() => { OnClickQuit(); });
        onCancelButton.onClick.AddListener(() => { OnClickCancel(); });

        TextUpdate();
        OffPanel();

        base.InitValue(); 
    }
        
    protected override void TextUpdate()
    {
        
    }
    
    public override void OnPanel()
    {
        TextUpdate();
        base.OnPanel();
    }

    public override void OffPanel()
    {
        // TODO : 패널 초기화 스크립트 작성

        

        base.OffPanel();
    }
    #endregion

#region 메소드
    private bool ApplicationQuit()
    {
        if(!isApplicationQuit)
        {
            quitAct?.Invoke(true);
        }

        return isApplicationQuit;
    }

    private void OnClickQuit()
    {
        #if UNITY_EDITOR
        Debug.Log("종료");
        #endif

        isApplicationQuit = true;
        Application.Quit();
    }

    private void OnClickCancel()
    {
        isApplicationQuit = false;
        quitAct?.Invoke(false);
    }  
#endregion
}
