using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_Control : MonoBehaviour
{
    public void InputKey()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // TODO : Talk ม๘วเ
            Speech_Management.talk_Act?.Invoke();
        }
    }
}
