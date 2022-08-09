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
        staff,      //�������� �����ϰ� ��ġ�ϴ� ������
        make,       //������ �����ϴ� ������
        setting     //����â�� ���� ������
    }

    public Npctype npctype;

    private void OnMouseDown()
    {
        switch (npctype)
        {   
            case Npctype.staff:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���Ͻô� ��ư�� �������ּ���.");
                staffPanel.SetActive(true);
                break;
            case Npctype.make:
                break;
            case Npctype.setting:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "������ �Ϸ��Ͻø� ��ȭâ�� ��ġ���ּ���.");
                settingPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
