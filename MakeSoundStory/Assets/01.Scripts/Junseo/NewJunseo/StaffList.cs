using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class StaffList : MonoBehaviour
{
    [SerializeField] private GameObject staffInfoPanel;
    [SerializeField] private Transform staffTrm;
    [SerializeField] private Transform staffPanelTrm;
    [SerializeField] private GameObject[] statFills;
    [SerializeField] private GameObject[] statBackFills;
    [SerializeField] private Text[] statTexts;
    [SerializeField] private Text mainText;
    [SerializeField] private Text staffNameText;
    [SerializeField] private Text staffLevelText;
    [SerializeField] private Text staffJobText;
    [SerializeField] private Button levelupButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button levelupOkButton;
    [SerializeField] private Button levelupCloseButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private GameObject levelUpPrefab;

    [SerializeField] private GameObject levelUpGameObj;

    [SerializeField] private StaffSO nowStaff = null;

    [SerializeField] private int staffIndex;

    private int increment = 0;

    private void Start()
    {
        Camera.main.GetComponent<CameraSetting>().enabled = false;
        StaffInfo(StaffManager.instance.workStaffList[0]);
        ButtonSetting();
    }

    public void Update()
    {

    }

    public void ButtonSetting()
    {
        closeButton.onClick.AddListener(() => staffInfoPanel.SetActive(false)); Camera.main.GetComponent<CameraSetting>().enabled = true;
        leftButton.onClick.AddListener(() =>
        {
            if (staffIndex != 0)
            {
                staffIndex--;
                StaffInfo(StaffManager.instance.workStaffList[staffIndex]);
            }
            else
            {
                staffIndex = StaffManager.instance.workStaffList.Count - 1;
                StaffInfo(StaffManager.instance.workStaffList[staffIndex]);
            }
        });
        rightButton.onClick.AddListener(() =>
        {
            if (staffIndex != StaffManager.instance.workStaffList.Count - 1)
            {
                staffIndex++;
                StaffInfo(StaffManager.instance.workStaffList[staffIndex]);
            }
            else
            {
                staffIndex = 0;
                StaffInfo(StaffManager.instance.workStaffList[staffIndex]);
            }
        });
        levelupButton.onClick.AddListener(() => { LevelUP(); });
        levelupOkButton.onClick.AddListener(() =>
        {
             LevelupFill();
        });
        levelupCloseButton.onClick.AddListener(() =>
        {
            levelupButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(true);
            levelupOkButton.gameObject.SetActive(false);
            levelupCloseButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            for(int i = 0; i < statBackFills.Length; i++)
            {
                statBackFills[i].SetActive(false); 
            }
            StaffInfo(nowStaff);
        });
    } 

    public void StaffInfo(StaffSO staff)
    {
        if (staffTrm.childCount != 0)
        {
            Destroy(staffTrm.GetChild(0).gameObject);
        }
        mainText.text = "스태프 목록";
        nowStaff = staff;
        staffNameText.text = staff.StaffName;
        staffLevelText.text = "Lv " + staff.StaffLevel;
        staffJobText.text = staff.StaffJob;
        for (int i = 0; i < statTexts.Length; i++)
        { 
            statTexts[i].text = staff.GetInfos()[i + 2].ToString();
        }
        for(int i = 0; i < statFills.Length; i++)
        {
             fillSetting(statFills[i], int.Parse(staff.GetInfos()[i + 2].ToString()));
        }
        GameObject miniStaff = Instantiate(staff.StaffPrefab, staffTrm.position, Quaternion.identity);
        miniStaff.transform.GetChild(0).gameObject.GetComponent<SortingGroup>().sortingOrder = 3;
        miniStaff.transform.parent = staffTrm;
        miniStaff.transform.localScale = new Vector2(3f, 3f); 
    }

    public void fillSetting(GameObject statFill, int stat)
    {
        statFill.GetComponent<SpriteRenderer>().size = new Vector2((stat / (100 / 100)) * 0.02f, 0.27f);
    }

    public void LevelUP()
    {
        if (nowStaff.StaffLevel != 5)
        {
            mainText.text = "레벨업";
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            LevelUpPanelSetting(nowStaff);
        }
        else
        {
            UIManager.instance.ShowExplane("스태프가 이미 최대 레벨입니다.");
        }
    }

    public void LevelUpPanelSetting(StaffSO staff)
    {
        switch (staff.StaffLevel)
        {
            case 1:
                increment = 5;
                BackFillSetting(staff, increment);
                break;
            case 2:
                increment = 10;
                BackFillSetting(staff, increment);
                break;
            case 3:
                increment = 15;
                BackFillSetting(staff, increment);
                break;
            case 4:
                increment = 20;
                BackFillSetting(staff, increment);
                break;
        }

    }
     
    public void BackFillSetting(StaffSO staff, int increment)
    {
        staffLevelText.text = "Lv " + (staff.StaffLevel + 1);
        levelupButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

        levelupOkButton.gameObject.SetActive(true);
        levelupCloseButton.gameObject.SetActive(true); 

        for (int i = 0; i < statBackFills.Length; i++)
        {
            statBackFills[i].SetActive(true);
            fillSetting(statBackFills[i], int.Parse(staff.GetInfos()[i + 2].ToString()) + increment);
        } 
        for (int i = 0; i < statTexts.Length; i++)
        {
            statTexts[i].text = (int.Parse(staff.GetInfos()[i + 2].ToString()) + increment).ToString();
        }
    }

    public void LevelupFill() 
    {
        for (int i = 0; i < statBackFills.Length; i++)
        {
            statBackFills[i].SetActive(false);
        }
            for (int i = 0; i < statFills.Length; i++)
        {
            statFills[i].GetComponent<SpriteRenderer>().size = statBackFills[i].GetComponent<SpriteRenderer>().size;
        }
            
        Sequence mySequence = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() => { 
               
            })
            .SetDelay(2f);

        mySequence.Restart();

        mainText.text = "스태프 목록";
        levelupButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        levelupOkButton.gameObject.SetActive(false);
        levelupCloseButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        nowStaff.StaffLevel += 1;
        nowStaff.Addictive += increment;
        nowStaff.Creativity += increment;
        nowStaff.Melodic += increment;
        nowStaff.Popularity += increment;
    }
}
