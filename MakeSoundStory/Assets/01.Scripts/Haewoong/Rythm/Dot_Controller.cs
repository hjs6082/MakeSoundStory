using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot_Controller : MonoBehaviour
{
    private Dictionary<KeyCode, int> Dot_Key_Dic = null;

    public bool isCols = false;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        if(isCols)
        {
            InputDot();
        }
    }

    private void InitValue()
    {
        Dot_Key_Dic = new Dictionary<KeyCode, int>();

        Dot_Key_Dic.Add(KeyCode.UpArrow,    0);
        Dot_Key_Dic.Add(KeyCode.DownArrow,  1);
        Dot_Key_Dic.Add(KeyCode.LeftArrow,  2);
        Dot_Key_Dic.Add(KeyCode.RightArrow, 3);
    }

    private void InputDot()
    {
        if (Input.anyKeyDown)
        {
            foreach (var item in Dot_Key_Dic)
            {
                if(Input.GetKeyDown(item.Key))
                {
                    isCols = false;
                    Dot_Management.Instance.addScore_Act?.Invoke(item.Key, item.Value);
                    return;
                }
            }
        }
    }
}
