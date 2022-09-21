using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficePanel : StaffPanel
{
    public override Action init { get; set; } = null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;

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
        TextUpdate();
        OffPanel();
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
}
