using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPanel : MonoBehaviour
{
    public StaffPanel employPanel = null;

    [SerializeField] private Button employButton = null;
    
    [SerializeField] private StaffResume[] staffResumes = null;
    public StaffSO curSelectStaff = null;

    private void Awake()
    {
         
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            List<StaffSO> staffList = new List<StaffSO>(StaffManager.instance.staffList);

            for(int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
            {
                staffList.Remove(StaffManager.instance.workStaffList[i]);
            }

            for(int i = 0; i < staffList.Count; i++)
            {
                StaffManager.instance.workStaffList.Add(staffList[i]);
            }

            employPanel.OffPanel();
        }
    }

    public void InitValue()
    {
        employPanel = UIManagement.instance.GetStaffPanel<EmployPanel>();

        employButton.onClick.AddListener(() => { Employ(); });

        OffPanel();
    }

    public void OnPanel()
    {
        this.gameObject.SetActive(true);

        for(int i = 0; i < staffResumes.Length; i++)
        {
            staffResumes[i].bSelected = false;
            staffResumes[i].StateUpdate();
        }

        DrawRandomStaff();
    }

    public void OffPanel()
    {
        employPanel.OffPanel();
        this.gameObject.SetActive(false);
    }

    public void Employ()
    {
        if(curSelectStaff != null)
        {
            if(GameManager.instance.playerMoney >= curSelectStaff.Money)
            {
                GameManager.instance.playerMoney -= curSelectStaff.Money;

                UIManagement.instance.MoneyTextUpdate();
                
                StaffManager.instance.workStaffList.Add(curSelectStaff);


                OffPanel();
            }
            else
            {
                employPanel.Warning();
            }            
            // TODO : 돈 부족 Warning 띄우기

        }
    }

    public void DrawRandomStaff()
    {
        List<StaffSO> staffList = new List<StaffSO>(StaffManager.instance.staffList);

        for(int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
        {
            staffList.Remove(StaffManager.instance.workStaffList[i]);
        }

        for(int i = 0; i < staffResumes.Length; i++)
        {
            for(int j = 0; j < staffResumes.Length; j++)
            {
                if(staffResumes[j].staffInfo != null)
                {
                    staffList.Remove(staffResumes[j].staffInfo);
                }
            }

            int rand = Random.Range(0, staffList.Count);

            staffResumes[i].SetStaff(staffList[rand]);
        }
    }

    public void SelectStaff(StaffSO _staffInfo)
    {
        for(int i = 0; i < staffResumes.Length; i++)
        {
            staffResumes[i].bSelected = false;
            staffResumes[i].StateUpdate();
        }

        curSelectStaff = _staffInfo;
    }
}
