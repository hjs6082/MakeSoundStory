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
                Setting(staffPanel, "원하시는 버튼을 선택해주세요."); 
                break;
            case Npctype.make:
                Setting(musicPanel, "음악 제작을 시작할까요?");
                break;
            case Npctype.setting:
                Setting(settingPanel, "설정을 완료해주세요.");               
                break;
            case Npctype.bank:
                Setting(bankPanel, "원하는 액수만큼 대출을 진행해주세요.");
                break;
            case Npctype.office:
                Setting(officePanel, "현재 회사 정보입니다."); 
                break;
            case Npctype.shop:
                Setting(shopPanel, "상점에서 필요한 아이템을 구매해주세요."); 
                break;
            default:
                break;
        }
    }
    
    public void Setting(GameObject panel, string sayText)
    {
        panel.SetActive(true);
        StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, sayText);
        Camera.main.GetComponent<CameraSetting>().enabled = false;
        StaffManager.instance.StopNpc();
    }

}


