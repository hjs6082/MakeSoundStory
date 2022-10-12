using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eStat
{
    CREATE = 0,
    ADDICT = 1,
    MELODI = 2,
    POPULA = 3
}

public class ListPanel : MonoBehaviour
{
#region Values
    [Header("스태프 썸네일")]
    [SerializeField] private RectTransform staffHeadTrm  = null;
    [SerializeField] private GameObject    staffHead     = null;
    [SerializeField] private StaffSO       curStaffSO    = null;

    private int curStaffIndex = 0;

    [Header("스태프 정보")]
    [SerializeField] private Text nameText  = null;
    [SerializeField] private Text levelText = null;
    [SerializeField] private Text jobText   = null;

    [SerializeField] private Button nextStaffButton = null;
    [SerializeField] private Button prevStaffButton = null;

    [Header("스태프 스탯 관련")]
    [SerializeField] private StatInfo[] statGuages = null;
    [SerializeField] private Button levelUpReadyButton   = null;
    [SerializeField] private Button levelUpButton   = null;
    [SerializeField] private Button levelUpCancleButton   = null;

    private bool readyToLevelup = false;

#endregion

#region Messeges
    private void Awake() {
        
    }

    private void Start() {
        
    }

    private void Update() {
        
    }
#endregion

#region Methods
    public void OnPanel()
    {
        this.gameObject.SetActive(true);

        SetStaff();
    }

    public void OffPanel()
    {
        curStaffIndex = 0;

        this.gameObject.SetActive(false);
    }

    public void InitValue()
    {
        for(int i = 0; i < statGuages.Length; i++)
        {
            statGuages[i].ownStat = (eStat)i;
        }

        nextStaffButton.onClick.AddListener(() => { NextStaff(); });
        prevStaffButton.onClick.AddListener(() => { PrevStaff(); });

        levelUpReadyButton.onClick.AddListener(() => { LevelUpReady(); });
        levelUpButton.onClick.AddListener(() => { LevelUp(); });
        levelUpCancleButton.onClick.AddListener(() => { LevelUpCancle(); });

        OffPanel();
    }

    private void SetStaff()
    {
        curStaffSO = StaffManager.instance.workStaffList[curStaffIndex];

        if(staffHead != null) { Destroy(staffHead); staffHead = null; } 
        staffHead = Instantiate<GameObject>(curStaffSO.StaffHeadPrefab, staffHeadTrm);

        nameText.text = curStaffSO.StaffName;
        levelText.text = $"Lv. {curStaffSO.StaffLevel}";
        jobText.text = curStaffSO.StaffJob;

        for(int i = 0; i < statGuages.Length; i++)
        {
            statGuages[i].InitValue(curStaffSO);
        }
    }

    private void NextStaff()
    {
        curStaffIndex++;

        if(curStaffIndex >= StaffManager.instance.workStaffList.Count)
        {
            curStaffIndex = 0;
        }

        curStaffSO = StaffManager.instance.workStaffList[curStaffIndex];

        SetStaff();
    }

    private void PrevStaff()
    {
        curStaffIndex--;

        if(curStaffIndex < 0)
        {
            curStaffIndex = StaffManager.instance.workStaffList.Count - 1;
        }

        curStaffSO = StaffManager.instance.workStaffList[curStaffIndex];

        SetStaff();
    }

    private void LevelUpReady()
    {
        levelUpReadyButton.gameObject.SetActive(false);

        for(int i = 0; i < statGuages.Length; i++)
        {
            statGuages[i].SetStatText($"+ {5 * curStaffSO.StaffLevel}");
            statGuages[i].BackGuageUpdate(curStaffSO.StaffLevel);
        }
    }

    private void LevelUp()
    {
        curStaffSO.StaffLevel++;
        levelText.text = $"Lv. {curStaffSO.StaffLevel}";

        curStaffSO.Addictive = Mathf.Clamp(curStaffSO.Addictive + (5 * curStaffSO.StaffLevel), 0, 100);
        curStaffSO.Creativity = Mathf.Clamp(curStaffSO.Creativity + (5 * curStaffSO.StaffLevel), 0, 100);
        curStaffSO.Melodic = Mathf.Clamp(curStaffSO.Melodic + (5 * curStaffSO.StaffLevel), 0, 100);
        curStaffSO.Popularity = Mathf.Clamp(curStaffSO.Popularity + (5 * curStaffSO.StaffLevel), 0, 100);

        LevelUpCancle();
    }

    private void LevelUpCancle()
    {
        for(int i = 0; i < statGuages.Length; i++)
        {
            statGuages[i].InitGuage();
        }

        levelUpReadyButton.gameObject.SetActive(true);
    }
#endregion
}
