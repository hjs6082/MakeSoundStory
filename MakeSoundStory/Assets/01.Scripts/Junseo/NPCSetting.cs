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
        staff,      //스태프를 관리하고 배치하는 스태프
        make,       //음악을 제작하는 스태프
        setting     //설정창을 여는 스태프
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
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "원하시는 버튼을 선택해주세요.");
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
                StaffManager.instance.Talk(this.gameObject.GetComponent<StaffData>().myStaffData, "설정을 완료하시면 대화창을 터치해주세요.");
                settingPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
