using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Button xButton;
    // Start is called before the first frame update
    void Start()
    {
        SettingStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingStart()
    {
        xButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false); 
            Camera.main.GetComponent<CameraSetting>().enabled = true;
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
    }
}
