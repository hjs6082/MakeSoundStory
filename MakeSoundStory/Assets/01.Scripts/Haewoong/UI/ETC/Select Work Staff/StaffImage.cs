using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffImage : MonoBehaviour
{
    private MusicPanel musicPanel = null;

    public StaffSO ownStaffSO = null;
    public Text ownStaffNameText = null;
    public Button ownSelectButton = null;

    private void Start()
    {
        //musicPanel = UIManagement.instance.staffPanels[2].GetComponent<MusicPanel>();
        ownSelectButton = GetComponent<Button>();
        ownSelectButton.onClick.AddListener(() => {
            ProfileButton profile = MusicPanel.instance.curProfile.GetComponent<ProfileButton>();

            profile.ProfileUpdate(ownStaffSO);

            MusicPanel.instance.OffSelectPanel();
        });  
    }


}
