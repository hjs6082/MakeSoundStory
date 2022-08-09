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

    public enum Npctype
    {
        staff,      //스태프를 관리하고 배치하는 스태프
        make,       //음악을 제작하는 스태프
        setting     //설정창을 여는 스태프
    }

    public Npctype npctype;

    private void OnMouseDown()
    {
        switch (npctype)
        {   
            case Npctype.staff:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "원하시는 버튼을 선택해주세요.");
                staffPanel.SetActive(true);
                break;
            case Npctype.make:
                break;
            case Npctype.setting:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "설정을 완료하시면 대화창을 터치해주세요.");
                settingPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
