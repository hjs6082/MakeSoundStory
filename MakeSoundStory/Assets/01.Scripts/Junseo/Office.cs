using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office : MonoBehaviour
{
    [SerializeField]
    private Button xButton;

    private void Start()
    {
        officeSetting();    
    }

    public void officeSetting()
    {
        xButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
    }
}
