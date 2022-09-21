using System; 
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployWarningPanel : MonoBehaviour
{
    [SerializeField] private Button toLoanBtn = null;
    [SerializeField] private Button backBtn = null;

    public void InitValue()
    {
        toLoanBtn.onClick.AddListener(() => {
            ToLoan();
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

    private void ToLoan()
    {
        Back();
        UIManagement.instance.GetStaffPanel<BankPanel>().OnPanel();
    }

    private void Back()
    {
        UIManagement.instance.GetStaffPanel<EmployPanel>().OffPanel();
    }
}
