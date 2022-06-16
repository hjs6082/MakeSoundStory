using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button loadButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    [SerializeField]
    private GameObject howtoplayPanel;

    [SerializeField]
    private GameObject clearPanel;

    [SerializeField]
    [TextArea] private string[] explaneText;

    [SerializeField]
    private Sprite[] explaneSprite;

    [SerializeField]
    private int nowNumber = 0;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("이미 메인매니저가 있습니다.");
        }
        ButtonSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSetting()
    {
        startButton.onClick.AddListener(() => { HowtoPlay(); });
        loadButton.onClick.AddListener(() => { });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
        leftButton.onClick.AddListener(() => { LeftClick(); });
        rightButton.onClick.AddListener(() => { RightClick(); });
    }

    public void HowtoPlay()
    {
        howtoplayPanel.SetActive(true);
        howtoplayPanel.transform.GetChild(1).GetComponent<Text>().text = "-" + nowNumber + "-";
        howtoplayPanel.transform.GetChild(2).GetComponent<Image>().sprite = explaneSprite[nowNumber];
        howtoplayPanel.transform.GetChild(3).GetComponent<Text>().text = explaneText[nowNumber];
    }

    public void RightClick()
    {
        if (nowNumber == 8)
        {
            clearPanel.SetActive(true);
            clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
            {
                clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
                SceneManager.LoadScene("SceneJunseo");
            });
        }
        else
        {
            nowNumber++;
            howtoplayPanel.transform.GetChild(3).GetComponent<Text>().text = explaneText[nowNumber];
            howtoplayPanel.transform.GetChild(2).GetComponent<Image>().sprite = explaneSprite[nowNumber];
            howtoplayPanel.transform.GetChild(1).GetComponent<Text>().text = "-" + nowNumber + "-";
        }
    }

    public void LeftClick()
    {
        if (nowNumber > 0)
        {
            nowNumber--;
            howtoplayPanel.transform.GetChild(3).GetComponent<Text>().text = explaneText[nowNumber];
            howtoplayPanel.transform.GetChild(2).GetComponent<Image>().sprite = explaneSprite[nowNumber];
            howtoplayPanel.transform.GetChild(1).GetComponent<Text>().text = "-" + nowNumber + "-";
        }
    }
}
