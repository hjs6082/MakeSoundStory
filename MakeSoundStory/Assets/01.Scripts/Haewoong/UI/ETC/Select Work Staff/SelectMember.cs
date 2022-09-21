using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMember : MonoBehaviour
{
    private const int STAT_COUNT = 4;
    public int[] stats = null;
    public Text[] statTexts = null;

    public GameObject selectPanel = null;
    public GameObject staffImagePrefab = null;
    public RectTransform staffImageParent = null;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    private void InitValue()
    {
        stats = new int[STAT_COUNT];

        InitSelectPanel();
    }

    private void InitSelectPanel()
    {
        List<StaffSO> staffSOs = new List<StaffSO>(StaffManager.instance.workStaffList);

        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            staffSOs.Remove(StaffManager.instance.pickWorkStaffList[i]);
        }

        for(int i = 0; i < staffSOs.Count; i++)
        {
            GameObject staffObj = Instantiate(staffImagePrefab, staffImageParent);
            StaffImage staffImg = staffObj.GetComponent<StaffImage>();

            staffImg.ownStaffSO = staffSOs[i];
            staffImg.ownStaffNameText.text = staffImg.ownStaffSO.StaffName;
        }
    }

    private void StatUpdate(int _create, int _addict, int _melodic, int _popular)
    {
        int[] _stats = {_create, _addict, _melodic, _popular};

        for(int i = 0; i < STAT_COUNT; i++)
        {
            stats[i] += _stats[i];
        }

        for(int i = 0; i < STAT_COUNT; i++)
        {
            UIManagement.TextUpdate(statTexts[i], stats[i].ToString());
        }
    }
}
