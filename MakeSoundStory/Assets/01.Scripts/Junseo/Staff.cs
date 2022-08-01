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
                //���콺Ŀ���� ��ȭ������� �ٲ���.
                StaffManager.instance.NoneTalk(this.gameObject.GetComponent<StaffData>().myStaffData);
                break;
            case status.talk:
                break;
            default:
                break;
        }
    }
}
