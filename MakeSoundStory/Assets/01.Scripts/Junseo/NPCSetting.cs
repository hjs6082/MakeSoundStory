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
        staff,      //�������� �����ϰ� ��ġ�ϴ� ������
        make,       //������ �����ϴ� ������
        setting,    //����â�� ���� ������
        bank,        //���� �����ϴ� ������  
        office,      //ȸ�� ������ Ȯ���ϴ� ������
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
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���Ͻô� ��ư�� �������ּ���.");
                staffPanel.SetActive(true);
                StaffManager.instance.StopNpc();
                staffPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.YesGacha();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                staffPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    UIManager.instance.showDogam();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                staffPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                break;
            case Npctype.make:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���� ������ �����ұ��?");
                musicPanel.SetActive(true);
                StaffManager.instance.StopNpc();
                musicPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => 
                {   musicPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    UIManager.instance.MakeMusicStart();
                });
                musicPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => 
                {
                    StaffManager.instance.RunNpc(); 
                    musicPanel.SetActive(false); 
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            case Npctype.setting:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "������ �Ϸ����ּ���.");
                Camera.main.GetComponent<CameraSetting>().enabled = false;
                settingPanel.SetActive(true);
                StaffManager.instance.StopNpc();
                settingPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    settingPanel.SetActive(false);
                    Camera.main.GetComponent<CameraSetting>().enabled = true;
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                break;
            case Npctype.bank:
                int bankMoney = 500;
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���ϴ� �׼���ŭ ������ �������ּ���.");
                bankPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = bankMoney + GameManager.instance.playerMoney + "G";
                bankPanel.SetActive(true);
                StaffManager.instance.StopNpc();
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
                    StaffManager.instance.RunNpc();
                });
                bankPanel.transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    bankPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                break;
            case Npctype.office:
                officePanel.SetActive(true);
                StaffManager.instance.StopNpc();
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���� ȸ�� �����Դϴ�.");
                officePanel.GetComponent<Button>().onClick.AddListener(() =>
                {
                    officePanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                break;
            case Npctype.shop:
                shopPanel.SetActive(true);
                Camera.main.GetComponent<CameraSetting>().enabled = false;
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "�������� �ʿ��� �������� �������ּ���.");
                StaffManager.instance.StopNpc();
                shopPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    shopPanel.SetActive(false);
                    Camera.main.GetComponent<CameraSetting>().enabled = true;
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    StaffManager.instance.RunNpc();
                });
                break;
            default:
                break;
        }
    }
}
