using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public GameObject nameSettingPanel;
    public GameObject RequestionPanel;
    public Text officeName;

    public Image logoSprite;
    public Button[] buttons;
    public InputField officeNameInputField;
   

    void Start()
    {
        LogoMove();
    }

    public void LogoMove()
    {
        logoSprite.transform.DOLocalMoveY(931f, 3f).OnComplete(() => { StartCoroutine(LogoAnimation()); }) ;
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.DOLocalMoveX(0f, 3f); 
        }
    } 

    IEnumerator LogoAnimation()
    {
        logoSprite.transform.DOScale(new Vector2(2.1f, 2.1f), 0.5f).OnComplete(() =>
        { 
            logoSprite.transform.DOScale(new Vector2(2.0f, 2.0f), 0.5f).OnComplete(() => { StartCoroutine(LogoAnimation()); });
        });
        yield return new WaitForSeconds(0.1f);
    }

    public void OfficeNameSetting()
    {
        nameSettingPanel.SetActive(true); 
    }

    public void OfficeNameReQuestion()
    {
        RequestionPanel.SetActive(true);
        officeName.text = "'" + officeNameInputField.text + "'";
    }
    
    public void ReturnOfficeName()
    {
        RequestionPanel.SetActive(false);
        officeNameInputField.text = "";
    }

    public void ReturnMainPanel()
    {
        RequestionPanel.SetActive(false);
        nameSettingPanel.SetActive(false); 
    }

    public void GameStart()
    {
        LoadingSceneManager.LoadScene("RealMainScene_W_UI");
    }

    public void Load()
    {
        //������ �ε��Ұ�
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
