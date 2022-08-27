using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSetting : MonoBehaviour
{
    [SerializeField]
    private GameObject staffPanel;

    [SerializeField]
    private GameObject settingPanel;

    [SerializeField]
    private GameObject musicPanel;

    [SerializeField]
    private GameObject bankPanel;

    [SerializeField]
    private GameObject officePanel;

    [SerializeField]
    private GameObject shopPanel;

    public enum Npctype
    {
        staff,      //스태프를 관리하고 배치하는 스태프
        make,       //음악을 제작하는 스태프
        setting,    //설정창을 여는 스태프
        bank,        //돈을 대출하는 스태프  
        office,      //회사 정보를 확인하는 스태프
        shop
    }

    public Npctype npctype;

    private void Start()
    {
        staffPanel = UIManager.instance.staffNpcPanel;
        settingPanel = UIManager.instance.settingNpcPanel;
        musicPanel = UIManager.instance.musicNpcPanel;
        bankPanel = UIManager.instance.bankNpcPanel;
        officePanel = UIManager.instance.officeNpcPanel;
        shopPanel = UIManager.instance.shopNpcPanel;
    }

    private void OnMouseDown()
    {
        switch (npctype)
        {
            case Npctype.staff:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "원하시는 버튼을 선택해주세요.");
                staffPanel.SetActive(true);
                staffPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.YesGacha();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                staffPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    UIManager.instance.showDogam();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                staffPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            case Npctype.make:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "음악 제작을 시작할까요?");
                musicPanel.SetActive(true);
                musicPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => 
                {   musicPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    UIManager.instance.MakeMusicStart();
                });
                musicPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => { musicPanel.SetActive(false); UIManager.instance.staffTalkPanel.SetActive(false); });
                break;
            case Npctype.setting:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "설정을 완료해주세요.");
                Camera.main.GetComponent<CameraSetting>().enabled = false;
                settingPanel.SetActive(true);
                settingPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    settingPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false); 
                });
                break;
            case Npctype.bank:
                int bankMoney = 500;
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "원하는 액수만큼 대출을 진행해주세요.");
                bankPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = bankMoney + GameManager.instance.playerMoney + "G";
                bankPanel.SetActive(true);
                bankPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = GameManager.instance.playerMoney.ToString();
                bankPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = bankMoney + "G";
                bankPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (bankMoney != 30000)
                    {
                        bankMoney += 500;
                        bankPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = bankMoney + "G";
                        bankPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = bankMoney + GameManager.instance.playerMoney + "G";
                    }
                });
                bankPanel.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (bankMoney != 500)
                    {
                        bankMoney -= 500;
                        bankPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = bankMoney + "G";
                        bankPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = bankMoney + GameManager.instance.playerMoney + "G";
                    }
                });
                bankPanel.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.instance.playerMoney += bankMoney;
                    bankPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                bankPanel.transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    bankPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            case Npctype.office:
                officePanel.SetActive(true);
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "현재 회사 정보입니다.");
                officePanel.GetComponent<Button>().onClick.AddListener(() =>
                {
                    officePanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            case Npctype.shop:
                shopPanel.SetActive(true);
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "상점에서 필요한 아이템을 구매해주세요.");
                shopPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    shopPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            default:
                break;
        }
    }
}
