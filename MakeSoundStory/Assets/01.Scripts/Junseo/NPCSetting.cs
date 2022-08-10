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

    public enum Npctype
    {
        staff,      //�������� �����ϰ� ��ġ�ϴ� ������
        make,       //������ �����ϴ� ������
        setting     //����â�� ���� ������
    }

    public Npctype npctype;

    private void Start()
    {
        staffPanel = UIManager.instance.staffNpcPanel;
        settingPanel = UIManager.instance.settingNpcPanel;
        musicPanel = UIManager.instance.musicNpcPanel;
    }

    private void OnMouseDown()
    {
        switch (npctype)
        {
            case Npctype.staff:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���Ͻô� ��ư�� �������ּ���.");
                staffPanel.SetActive(true);
                staffPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.YesGacha();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                staffPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    UIManager.instance.showStaffList();
                    staffPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                });
                break;
            case Npctype.make:
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "���� ������ �����ұ��?");
                musicPanel.SetActive(true);
                musicPanel.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => 
                {   musicPanel.SetActive(false);
                    UIManager.instance.staffTalkPanel.SetActive(false);
                    UIManager.instance.MakeMusicStart();
                });
                musicPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => { musicPanel.SetActive(false); UIManager.instance.staffTalkPanel.SetActive(false); });
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
