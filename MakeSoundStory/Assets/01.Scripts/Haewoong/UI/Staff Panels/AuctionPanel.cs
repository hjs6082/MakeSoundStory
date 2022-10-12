using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionPanel : StaffPanel
{
    public override Action init { get; set; }
    public override Button exitButton { get; set; }

    [SerializeField] private Button exitBtn = null;
    
    public Auction auction = null;

    protected override void Awake()
    {
        exitButton = exitBtn;
        base.Awake();
    }

    protected override void Update()
    {
        return;
    }

    protected override void InitValue()
    {
        TextUpdate();
        OffPanel();

        base.InitValue();
    }

    public override void OnPanel()
    {
        base.OnPanel();

        auction.AuctionStart();
    }

    public override void OffPanel()
    {
        base.OffPanel();
    }

    public override void Warning()
    {
        base.Warning();
    }

    protected override void TextUpdate()
    {
        return;
    }
}
