using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public static StaffManager instance;

    public List<StaffSO> staffList = new List<StaffSO>();

    public List<StaffSO> pickStaffList = new List<StaffSO>();

    public List<StaffSO> workStaffList = new List<StaffSO>();

    public int isSelectStaff;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("이미 스태프매니저가 있습니다.");
        }
        else
        {
            instance = this;
        }
        AddStaff();
    }

    void Update()
    {
    }

    public void AddStaff()
    {
        Debug.Log("234");
        StaffSO[] staffs = (Resources.LoadAll<StaffSO>("StaffSO"));
        for(int i = 0; i < staffs.Length; i++)
        {
            staffList.Add(staffs[i]);
        }

        /*foreach (var item in staffList)
        {
           Debug.Log(item.name);
        }*/
    }

    public void RandomStaff()
    {
        if (staffList != null)
        {
            int randoxIndex = Random.Range(0, staffList.Count);
            StaffSO selectStaff = staffList[randoxIndex];
            pickStaffList.Add(staffList[randoxIndex]);
            staffList.RemoveAt(randoxIndex);

            UIManager.instance.StaffGatcha(selectStaff);

            if (workStaffList.Count != 0)
            {
                Debug.Log("243");
                staffList.Clear();
                StaffSO[] staffs = (Resources.LoadAll<StaffSO>("StaffSO"));
                for (int i = 0; i < staffs.Length; i++)
                {
                    staffList.Add(staffs[i]);
                }

                for (int i = 0; i < workStaffList.Count; i++)
                {
                    staffList.Remove(workStaffList[i]);
                }
            }
            if(pickStaffList.Count == 6)
            {
                pickStaffList.Clear();
                staffList.Clear();
                StaffSO[] staffs = (Resources.LoadAll<StaffSO>("StaffSO"));
                for (int i = 0; i < staffs.Length; i++)
                {
                    staffList.Add(staffs[i]);
                }

                for (int i = 0; i < workStaffList.Count; i++)
                {
                    staffList.Remove(workStaffList[i]);
                }
            }  

        }
    }
}
