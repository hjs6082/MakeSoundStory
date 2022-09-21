using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionWarningPanel : MonoBehaviour
{
    [SerializeField] private Button toEmployBtn = null;
    [SerializeField] private Button backBtn = null;

    public void InitValue()
    {
        toEmployBtn.onClick.AddListener(() => {
            ToEmploy();
        });

        backBtn.onClick.AddListener(() => {
            Back();
        });

        OffPanel();
    }

    public void OnPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void OffPanel()
    {
        this.gameObject.SetActive(false);
    }

    private void ToEmploy()
    {
        UIManagement.instance.GetStaffPanel<MusicPanel>().OffPanel();
        UIManagement.instance.GetStaffPanel<EmployPanel>().OnPanel();
    }

    private void Back()
    {
        OffPanel();
    }
}
