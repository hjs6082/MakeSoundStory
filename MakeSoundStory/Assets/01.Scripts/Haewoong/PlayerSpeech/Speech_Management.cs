using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speech_Management : MonoBehaviour
{
    public enum eFocus
    {
        DATE,
        MONEY
    }

    private Action input_Act = null;
    public static Action talk_Act = null;

    private Speech_Control speech_Ctrl = null;
    public Speech_Control Speech_Ctrl => speech_Ctrl;

    private Speech_Func speech_Func = null;
    public Speech_Func Speech_Func => speech_Func;

    public int curStrsIndex = 0;
    public Queue<string> curStrs = null;
    public TextMeshProUGUI speech_TMP = null;
    public static bool isTalking = false;

    public int speechCount = 0;
    public GameObject[] focusPanels = null;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        if(!isTalking)
        {
            input_Act?.Invoke();
        }
    }

    private void InitValue()
    {
        speech_Ctrl = FindObjectOfType<Speech_Control>();
        speech_Func = FindObjectOfType<Speech_Func>();

        input_Act += () => speech_Ctrl.InputKey();
        talk_Act += () => 
        {
            Tutorial();
        };

        speech_TMP.text = string.Empty;
        curStrs = Speech_Strs.GetTurotialStrsToQueue(curStrsIndex + 1);

        for(int i = 0; i < focusPanels.Length; i++)
        {
            focusPanels[i].SetActive(false);
        }
    }

    private void Tutorial()
    {
        speechCount++;

        switch (speechCount)
        {
            case 3: ChangeFocus(eFocus.DATE); break;
            case 4: ChangeFocus(eFocus.MONEY); break;
            case 5: ClearFocus(); break;
            case 6:
            {
                Debug.Log("튜토 1 끝"); 
                focusPanels[0].transform.parent.parent.gameObject.SetActive(false);
                // TODO : 스태프실 ㄱ
            }break;
            default: break;
        }

        Talk();
    }

    private void Talk()
    {
        speech_Func.Talk(speech_TMP, curStrs.Dequeue());
        if (curStrs.Count == 0)
        {
            curStrsIndex++;
            curStrs = Speech_Strs.GetTurotialStrsToQueue(curStrsIndex + 1);
        }
    }

    private void ChangeFocus(eFocus _focus)
    {
        int focusIdx = (int)_focus;

        ClearFocus();
        focusPanels[focusIdx].SetActive(true);
    }

    private void ClearFocus()
    {
        for(int i = 0; i < focusPanels.Length; i++)
        {
            focusPanels[i].SetActive(false);
        }
    }
}
