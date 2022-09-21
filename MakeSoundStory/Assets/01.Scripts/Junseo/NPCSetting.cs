using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSetting : MonoBehaviour
{
    // [SerializeField]
    // private GameObject staffPanel;

    // [SerializeField]
    // private GameObject settingPanel;

    // [SerializeField]
    // private GameObject musicPanel;

    // [SerializeField]
    // private GameObject bankPanel;

    // [SerializeField]
    // private GameObject officePanel;

    // [SerializeField]
    // private GameObject shopPanel;

    // public enum Npctype
    // {
    //     staff,      //�������� �����ϰ� ��ġ�ϴ� ������
    //     make,       //������ �����ϴ� ������
    //     setting,    //����â�� ���� ������
    //     bank,        //���� �����ϴ� ������  
    //     office,      //ȸ�� ������ Ȯ���ϴ� ������
    //     shop
    // }

    // public Npctype npctype;

    // private void Start()
    // {
    //     staffPanel = UIManager.instance.staffNpcPanel;
    //     settingPanel = UIManager.instance.settingNpcPanel;
    //     musicPanel = UIManager.instance.musicNpcPanel;
    //     bankPanel = UIManager.instance.bankNpcPanel;
    //     officePanel = UIManager.instance.officeNpcPanel;
    //     shopPanel = UIManager.instance.shopNpcPanel;
    // }

    [SerializeField] private StaffPanel staffpanel = null;

    private void OnMouseDown()
    { 
        // switch (npctype)
        // {
        //     case Npctype.staff:
        //         Setting(staffPanel, "���Ͻô� ��ư�� �������ּ���."); 
        //         break;
        //     case Npctype.make:
        //         Setting(musicPanel, "���� ������ �����ұ��?");
        //         break;
        //     case Npctype.setting:
        //         Setting(settingPanel, "������ �Ϸ����ּ���.");               
        //         break;
        //     case Npctype.bank:
        //         Setting(bankPanel, "���ϴ� �׼���ŭ ������ �������ּ���.");
        //         break;
        //     case Npctype.office:
        //         Setting(officePanel, "���� ȸ�� �����Դϴ�."); 
        //         break;
        //     case Npctype.shop:
        //         Setting(shopPanel, "�������� �ʿ��� �������� �������ּ���."); 
        //         break;
        //     default:
        //         break;
        // }
    
        Setting(staffpanel, "");
    }
    
    public void Setting(StaffPanel panel, string sayText)
    {
        panel.OnPanel();
        StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, sayText);
        //Camera.main.GetComponent<CameraSetting>().enabled = false;
        StaffManager.instance.StopNpc();
    }

}


