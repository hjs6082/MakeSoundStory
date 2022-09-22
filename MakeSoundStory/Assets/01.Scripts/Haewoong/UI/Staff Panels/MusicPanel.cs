using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPanel : StaffPanel
{
    public static MusicPanel instance = null;

    public override Action init { get; set; }= null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;
    [SerializeField] private Button initBtn = null;
    [SerializeField] private Button makeBtn = null;
    [SerializeField] private CompleteMusicPanel completeMusic = null;
    [SerializeField] private ProductionWarningPanel warningPanel = null;

    public Button[] memberButton = null;
    public Button curProfile = null;

    public GameObject staffProfilePrefab = null;
    public RectTransform staffList = null;
    public List<GameObject> image_List = null;

    public List<StaffSO> selected_Staff_List = null;

    protected override void Awake()
    {
        exitButton = exitBtn;
        base.Awake();

        if(instance == null)
        {
            instance = this;
            return;
        }
    }

    protected override void Update()
    {
        
    }

    protected override void InitValue()
    {
        initBtn.onClick.AddListener(() => {
            //InitProfiles();
        });

        makeBtn.onClick.AddListener(() => {
            if(selected_Staff_List.Count < 3) { Warning(); }
            else { MusicOut(); };
        });

        image_List = new List<GameObject>();
        selected_Staff_List = new List<StaffSO>();

        completeMusic.InitValue();
        warningPanel.InitValue();

        OffSelectPanel();

        TextUpdate();
        OffPanel();

        base.InitValue(); 
    }
        
    protected override void TextUpdate()
    {
        
    }

    public override void OnPanel()
    {
        TextUpdate();
        base.OnPanel();
    }

    public override void OffPanel()
    {
        // TODO : 패널 초기화 스크립트 작성
        staffList.gameObject.SetActive(false);
        warningPanel.gameObject.SetActive(false);
        completeMusic.OffPanel();
    
        base.OffPanel();
    }

    public override void Warning()
    {
        warningPanel.OnPanel();
        
        base.Warning();
    }

#region 직원 선택
    public void OnSelectPanel()
    {
        staffList.gameObject.SetActive(true);

        StaffUpdate();
    }

    public void OffSelectPanel()
    {
        for(int i = 0; i < image_List.Count; i++)
        {

            Destroy(image_List[i]);
            
        }

        //InitProfiles();

        staffList.gameObject.SetActive(false);
    }

    public void StaffUpdate()
    {
        List<StaffSO> staff_List = new List<StaffSO>(StaffManager.instance.workStaffList);

        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            staff_List.Remove(StaffManager.instance.pickWorkStaffList[i]);
        }

        for(int i = 0; i < staff_List.Count; i++)
        {
            GameObject profile = Instantiate(staffProfilePrefab, staffList);

            StaffImage staffImg = profile.GetComponent<StaffImage>();
            staffImg.ownStaffSO = staff_List[i];
            profile.GetComponentInChildren<Text>().text = staffImg.ownStaffSO.StaffName;

            image_List.Add(profile);
        }
    }
#endregion

#region 음악 제작
    public void MusicOut()
    {
        float[] stats = new float[4];
        float[] allStats = new float[4];

        for(int i = 0; i < selected_Staff_List.Count; i++)
        {
            allStats[0] += selected_Staff_List[i].Creativity;
            allStats[1] += selected_Staff_List[i].Addictive;
            allStats[2] += selected_Staff_List[i].Melodic;
            allStats[3] += selected_Staff_List[i].Popularity;
        }

        for(int i = 0; i < stats.Length; i++)
        {
            float stat = allStats[i];
            stats[i] = Mathf.Round(UnityEngine.Random.Range(stat / 3, stat));
        }

        completeMusic.MakeComplete(stats, allStats);
    }

    // private void InitProfiles()
    // {
    //     for(int i = 0; i < memberButton.Length; i++)
    //     {
    //         ProfileButton profile = memberButton[i].GetComponent<ProfileButton>();

    //         profile.ProfileUpdate(null);
    //         profile.profileButton.interactable = true;
    //     }   
    // }
#endregion
}
