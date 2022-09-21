using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankPanel : StaffPanel
{
    public override Action init { get; set; }= null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;
    [SerializeField] private Button upBtn   = null;
    [SerializeField] private Button downBtn = null;
    [SerializeField] private Button loanBtn = null;

    [SerializeField] private Text curMoneyText = null;
    [SerializeField] private Text loanMoneyText = null;
    [SerializeField] private Text totalMoneyText = null;

    private int curLoanMoney = 500;

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
        curLoanMoney = 500;

        upBtn.onClick.AddListener(() => UpMoney());
        Debug.Log(1);
        downBtn.onClick.AddListener(() => DownMoney());
        Debug.Log(2);
        loanBtn.onClick.AddListener(() => Loan());
        Debug.Log(3);

        TextUpdate();
        OffPanel();

        base.InitValue(); 
    }

    protected override void TextUpdate()
    {
        curMoneyText.text = $"{GameManager.instance.playerMoney}G";
        loanMoneyText.text = $"{curLoanMoney}G";
        totalMoneyText.text = $"{GameManager.instance.playerMoney + curLoanMoney}G";

        UIManagement.instance.MoneyTextUpdate();
    }

    public override void OnPanel()
    {
        curLoanMoney = 500;
        TextUpdate();
        base.OnPanel();
    }

    public override void OffPanel()
    {
        // TODO : 패널 초기화 스크립트 작성
        TextUpdate();
        base.OffPanel();
    }

#region 버튼 기능

    private void UpMoney()
    {
        curLoanMoney = Mathf.Clamp(curLoanMoney + 500, 500, 7000);
        TextUpdate();
    }

    private void DownMoney()
    {
        curLoanMoney = Mathf.Clamp(curLoanMoney - 500, 500, 7000);
        TextUpdate();
    }

    private void Loan()
    {
        GameManager.instance.playerMoney += curLoanMoney;
        GameManager.instance.bankMoney += curLoanMoney;

        Debug.Log(GameManager.instance.playerMoney);
        
        OffPanel();
    }

#endregion
}
