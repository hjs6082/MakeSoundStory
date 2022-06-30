using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaffEventSO : ScriptableObject
{
    [Header("이벤트 내용")][SerializeField]
    [TextArea]private string staffEventContents;
    public string StaffEventContents { get { return staffEventContents; } }

}
