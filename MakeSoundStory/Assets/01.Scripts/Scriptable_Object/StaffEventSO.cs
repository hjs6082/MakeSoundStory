using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaffEventSO : ScriptableObject
{
    [Header("�̺�Ʈ ����")][SerializeField]
    [TextArea]private string staffEventContents;
    public string StaffEventContents { get { return staffEventContents; } }

}
