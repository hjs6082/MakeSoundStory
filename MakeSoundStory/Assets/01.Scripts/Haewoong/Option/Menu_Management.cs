using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Piano;

public class Menu_Management : MonoBehaviour
{
    [Header("메뉴 버튼")]
    [SerializeField] private Button optionBtn = null;
    [SerializeField] private Button pianoBtn = null;
    [SerializeField] private Button saveExitBtn = null;

    [Header("메뉴 패널")]
    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject optionPanel = null;
    [SerializeField] private GameObject pianoPanel = null;

    [Header("Piano 관련 변수들")]
    public Piano_KeyMap p_KeyMap = null;
    public float p_Volume = 0.0f;

    //[Header("Option 관련 변수들")]
    

    private void InitValue()
    {

    }

    private void InitPiano()
    {
        p_KeyMap = FindObjectOfType<Piano_KeyMap>();
        
    }

    private void OnOffMenu(bool _bOn)
    {
        optionPanel.SetActive(_bOn);
    }

    public void OnMenu()
    {
        OnOffMenu(true);
    }

    public void OffMenu()
    {
        OnOffMenu(false);
    }

    public void OpenPiano()
    {
        ClearOptionPanel();
        pianoPanel.SetActive(true);
    }

    public void OpenOption()
    {
        ClearOptionPanel();
        optionPanel.SetActive(true);
    }

    private void ClearOptionPanel()
    {
        pianoPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void InitialOption()
    {

    }

    private void SaveOptionValues()
    {

    }
}
