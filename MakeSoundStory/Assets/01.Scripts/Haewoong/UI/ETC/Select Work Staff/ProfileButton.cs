using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    //private MusicPanel musicPanel = null;

    public Button profileButton = null;
    public StaffSO profileSO = null;
    public Text profileNameText = null;

    private void Start()
    {
        profileButton = GetComponent<Button>();
        profileNameText = GetComponentInChildren<Text>();
        profileButton.onClick.AddListener(() => ClickProfile());
    }

    private void ClickProfile()
    {
        MusicPanel.instance.OnSelectPanel();
        MusicPanel.instance.curProfile = profileButton;
    }

    public void ProfileUpdate(StaffSO _staffSO)
    {
        StaffSO oldSO = profileSO;
        profileSO = _staffSO;

        if(oldSO != null)
        {
            StaffManager.instance.workStaffList.Add(oldSO);
            UIManagement.instance.GetStaffPanel<MusicPanel>().selected_Staff_List.Remove(oldSO);
        }
        
        UIManagement.instance.GetStaffPanel<MusicPanel>().selected_Staff_List.Add(profileSO);

        profileNameText.text = profileSO.StaffName;

        StaffManager.instance.pickWorkStaffList.Add(profileSO);

        profileButton.interactable = false;
    }
}
