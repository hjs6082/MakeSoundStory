using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Staff : MonoBehaviour 
{
    public GameObject myStatus;

    public Sprite noneTalkSprite = null;

    public enum status
    {
        none,
        talk
    }
    public status staffStatus;

    public void Click()
    {
        switch (staffStatus)
        {       
            case status.none:
                StaffManager.instance.NoneTalk(this.gameObject.GetComponent<StaffData>().myStaffData);
                break;
            case status.talk:
                break;
            default:
                break;
        }
    }

    private void OnMouseEnter()
    {
        myStatus.transform.position = new Vector2(this.gameObject.transform.position.x + 0.6f, this.gameObject.transform.position.y + 0.9f);
        switch (staffStatus)
        {
            case status.none:
                myStatus.GetComponent<SpriteRenderer>().sprite = noneTalkSprite;
                myStatus.SetActive(true); 
                break;
            case status.talk:
                break;
            default:
                break;
        }
    }
    private void OnMouseDown()
    {
        if (!UIManager.instance.staffTalkPanel.activeSelf)
        {
            Click();
        }
    }

    private void OnMouseExit()
    {
        myStatus.SetActive(false);
    }
}
