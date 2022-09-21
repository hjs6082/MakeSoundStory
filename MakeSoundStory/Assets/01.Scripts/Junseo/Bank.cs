using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] private Text plrGold;
    [SerializeField] private Text bankGold;
    [SerializeField] private Text endGold;
    [SerializeField] private Button goldUpButton;
    [SerializeField] private Button goldDownButton;
    [SerializeField] private Button sendButton;
    [SerializeField] private Button xButton;

    void Start()
    {
        BankStart();
    }


    public void BankStart()
    {
        endGold.text = GameManager.instance.bankMoney + GameManager.instance.playerMoney + "G";
        plrGold.text = GameManager.instance.playerMoney.ToString() + "G";
        bankGold.text = GameManager.instance.bankMoney + "G";
        goldUpButton.onClick.AddListener(() =>
        {
            if (GameManager.instance.bankMoney < 30000)
            {
                GameManager.instance.bankMoney += 500;
                bankGold.text = GameManager.instance.bankMoney + "G";
                endGold.text = GameManager.instance.bankMoney + GameManager.instance.playerMoney + "G";
            }
        });
        goldDownButton.onClick.AddListener(() =>
        {
            if (GameManager.instance.bankMoney > 0)
            {
                GameManager.instance.bankMoney -= 500;
                bankGold.text = GameManager.instance.bankMoney + "G";
                endGold.text = GameManager.instance.bankMoney + GameManager.instance.playerMoney + "G";
            }
        });
        sendButton.onClick.AddListener(() =>
        {
            GameManager.instance.playerMoney += GameManager.instance.bankMoney;
            GameManager.instance.playerDebt += GameManager.instance.bankMoney;
            GameManager.instance.bankMoney = 500;

            bankGold.text = 500 + "G";
            endGold.text = GameManager.instance.bankMoney + GameManager.instance.playerMoney + "G";
            plrGold.text = GameManager.instance.playerMoney.ToString() + "G"; 

            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false); 
            StaffManager.instance.RunNpc();
            
            UIManagement.instance.MoneyTextUpdate();
        });
        xButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
    }
}
