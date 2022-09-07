using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffOption : MonoBehaviour
{
    [SerializeField]
    private Button staffGachaButton;

    [SerializeField]
    private Button staffDogamButton;

    [SerializeField]
    private Button xButton;
    
    void Start()
    {
        StaffPanel();
    }

    void Update()
    {
        
    }

    public void StaffPanel()
    {
        staffGachaButton.onClick.AddListener(() => {
            UIManager.instance.YesGacha();
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
        staffDogamButton.onClick.AddListener(() =>
        {
            UIManager.instance.showDogam();
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
        xButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
    }
}
