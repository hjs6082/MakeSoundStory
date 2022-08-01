using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
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
                //마우스커서를 대화모양으로 바꿔줌.
                StaffManager.instance.NoneTalk(this.gameObject.GetComponent<StaffData>().myStaffData);
                break;
            case status.talk:
                break;
            default:
                break;
        }
    }
}
