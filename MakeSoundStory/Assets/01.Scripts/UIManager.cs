using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool isOneClick = false; // �ѹ��� �ǰ� �ϴ� bool �Լ��� ��)������ ������ ���������� ������ �ȵǱ⶧���� 1ȸ������ �δ� ����.

    private bool isSort = false; //Sort�� �ϴ°����� üũ�� bool;

    [SerializeField]
    private GameObject explanePanel; // �÷��̾� ���콺�� ����ٴϸ� ������ ���� ���� �г�.

    [SerializeField]
    private Text explaneText; //���� �ؽ�Ʈ ��) �������� �����մϴ�.

    [SerializeField]
    private Text moneyText; // ������ �ؽ�Ʈ

    [SerializeField]
    private GameObject companyPanel; //ȸ�� �г� 

    private int minusMoney;

    //---------������ �̱� ���� ����-----------
    [SerializeField]
    private Button gachaStartButton; //��í ��ŸƮ ��ư

    [SerializeField]
    private GameObject gachaGradePanel; //��í�� ����� ���ϴ� �г�

    [SerializeField]
    private Button[] gradeButtons;

    [SerializeField]
    private GameObject staffPanelObj;   //������ �г��� �ڽ����� �������ִ� �θ� ������Ʈ

    [SerializeField]
    private GameObject realPanelObj;   //������ ���ųİ� ����� ������Ʈ

    [SerializeField]
    private GameObject[] staffPanels; //�� 3���� �������� ��� �гε�;

    [SerializeField]
    private GameObject gradeSelectPanel;

    [SerializeField]
    private GameObject clearPanel; //���ᶧ ��� ������ �г�

    public GameObject staffGachaPanel; //������ ��í �г�

    //---------������ ���� ���� ����-----------
    public GameObject staffChoicePanelObj; // ������ ���� ��ü�г�
    
    public GameObject selectPanel; // �������� �����ϴ� �г�

    [SerializeField]
    private GameObject staffPrefab; // �������� �߰��� �뵵�� ����ϴ� ���ӿ�����Ʈ ������

    [SerializeField]
    private GameObject staffListPanel; //�������� �ڽ����� �����Ե� �г�

    [SerializeField]
    private Button[] buttons; //����� ������ �� �ִ� ��ư��

    [SerializeField]
    private Button pickUpExitButton; //������ ��ư

    [SerializeField]
    private Button makeMusicButton; //���� ����� ��ư

    [SerializeField]
    private Button choiceStaffButton; //������ ���� ��ư

    [SerializeField]
    private Button noChoiceStaffButton; //������ ���� ���� ��ư

    [SerializeField]
    private Button[] sortButtons;

    [SerializeField]
    private Text[] statTexts;

    [SerializeField]
    private Text sortText;

    public int buttonCount = 0;

    private int staffCount = 0;

    [SerializeField]
    private int memberCount = 0;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("�̹� UI�Ŵ����� �ֽ��ϴ�.");
        }
        else
        {
            instance = this;
        }
        ButtonClick();
    }

    void Update()
    {
        ClosePickUpPanel();
        TestPanel();
        Explane();
    }

    public void Explane()
    {
        explanePanel.transform.position = GetMousePosition(); 
    }

    public Vector2 GetMousePosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }

    public void ExplaneSetting(StaffSO staffSO)
    {
        explanePanel.SetActive(true);
        explanePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
        explanePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400);
        explanePanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "�̸�: " + staffSO.StaffName;
        explanePanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = staffSO.MySprite;
        explanePanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "���� : " + staffSO.StaffLevel;
        explanePanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = "��â�� : " + staffSO.Creativity;
        explanePanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "�ߵ��� : " + staffSO.Addictive;
        explanePanel.transform.GetChild(5).gameObject.GetComponent<Text>().text = "��ε��� : " + staffSO.Melodic;
        explanePanel.transform.GetChild(6).gameObject.GetComponent<Text>().text = "���߼� : " + staffSO.Popularity; 
    }

    public void ExplaneExit()
    {
        explanePanel.SetActive(false); 
    }

    public void GameStart()
    {
        GameManager.instance.playerMoney = 30000;
        companyPanel.SetActive(true);
        staffGachaPanel.SetActive(false);
        staffChoicePanelObj.SetActive(false);

        gradeButtons = new Button[gradeSelectPanel.transform.childCount];
        for (int i = 0; i < gradeSelectPanel.transform.childCount; i++)
        {
            gradeButtons[i] = gradeSelectPanel.transform.GetChild(i).GetComponent<Button>();
        } 
    }

    public void StaffGatcha(StaffSO selectStaff)
    {
        //Debug.Log("����");
        int staffPanelsCount = staffPanelObj.transform.childCount;
        
        staffPanels = new GameObject[staffPanelsCount];
        for(int i = 0; i < staffPanels.Length/* - 1*/; i++)
        {
            staffPanels[i] = staffPanelObj.transform.GetChild(i).gameObject;
        }

        StaffManager.instance.staffList.Remove(selectStaff);
        staffPanels[staffCount].transform.GetChild(0).GetComponent<Image>().sprite = selectStaff.MySprite;
        staffPanels[staffCount].transform.GetChild(1).GetComponent<Text>().text = "�̸� : " + selectStaff.StaffName;
        staffPanels[staffCount].transform.GetChild(2).GetComponent<Text>().text = "���� : " + selectStaff.StaffJob;
        staffPanels[staffCount].transform.GetChild(3).GetComponent<Text>().text = "�����ϴ� �帣 : " + selectStaff.FavoriteGenre;
        staffPanels[staffCount].transform.GetChild(4).GetComponent<Text>().text = "�Ⱦ��ϴ� �帣 : " + selectStaff.HateGenre;
        staffPanels[staffCount].transform.GetChild(6).GetComponent<Text>().text = "��â�� : " + selectStaff.Creativity;
        staffPanels[staffCount].transform.GetChild(7).GetComponent<Text>().text = "��ε��� : " + selectStaff.Melodic;
        staffPanels[staffCount].transform.GetChild(8).GetComponent<Text>().text = "�ߵ��� : " + selectStaff.Addictive;
        staffPanels[staffCount].transform.GetChild(9).GetComponent<Text>().text = "���߼� : " + selectStaff.Popularity;
        staffPanels[staffCount].transform.GetChild(10).GetComponent<Text>().text = "���� : " + selectStaff.Money + "G";
        staffPanels[staffCount].transform.GetChild(12).GetComponent<Text>().text = "���� : " + selectStaff.StaffLevel; 
        staffPanels[staffCount].GetComponent<MyData>().myStaff = selectStaff;

            if (staffCount != 3)
            {
                staffCount++;
                if (staffCount == 3)
                {
                    staffCount = 0;
                }
            }

    }

    public void SelectStaff(GameObject staffPanel)
    {
        for (int i = 0; i < staffPanels.Length; i++)
        {
            staffPanels[i].SetActive(false);
        }
        if (GameManager.instance.playerMoney >= staffPanel.GetComponent<MyData>().myStaff.Money)
        {
            GameManager.instance.playerMoney -= staffPanel.GetComponent<MyData>().myStaff.Money;
        }
        else
        {
            ShowExplane("���� �����մϴ�.");
        }
        staffPanel.SetActive(true);
        staffPanel.transform.DOScale(new Vector3(1.2f, 1.2f), 1.3f).OnComplete(() =>
        {
            ClearTween(staffGachaPanel);
            staffPanel.GetComponent<MyData>().isSelect = false; 
        });
    }

    public void StaffGachaEnd()
    {
        for(int i = 0; i < staffPanels.Length; i++)
        {
            Image image = staffPanels[i].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.42f;
            image.color = tempColor;
        }
        StaffManager.instance.isSelectStaff = 0; // ���� -1�̿���.
    }
     
    public void ClearTween(GameObject falsePanel)
    {
        clearPanel.SetActive(true);
        clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
        {
            falsePanel.SetActive(false);
            clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            isOneClick = false;
            StaffGachaEnd();
            companyPanel.SetActive(true);
        });
    }

    public void StaffGachaStart()
    {
        Debug.Log(23);
        for(int i = 0; i < staffPanels.Length; i++)
        {
            staffPanels[i].SetActive(true);     
            staffPanels[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        for (int i = 0; i < 3; i++)
        {
            StaffManager.instance.RandomStaff();
        }
    }



    //---------������ ���� ���� �Լ�-----------

    public void ButtonClick()
    {
        buttons[0].onClick.AddListener(() => { buttonCount = 1; SelectPanelClick(); });
        buttons[1].onClick.AddListener(() => { buttonCount = 2; SelectPanelClick(); });
        buttons[2].onClick.AddListener(() => { buttonCount = 3; SelectPanelClick(); });
        buttons[3].onClick.AddListener(() => { buttonCount = 4; SelectPanelClick(); });
        buttons[4].onClick.AddListener(() => { buttonCount = 5; SelectPanelClick(); });

        pickUpExitButton.onClick.AddListener(() => { PickUpStaffEnd(); });
        choiceStaffButton.onClick.AddListener(() => { MusicSceneChange(); /*ClearTween(staffChoicePanelObj);*/ });
        gachaStartButton.onClick.AddListener(() => { GachaGradeStart(); });
        makeMusicButton.onClick.AddListener(() => { MakeMusicStart(); });

    }

    public void MusicSceneChange()
    {
        if (memberCount >= 3)
        {
            companyPanel.SetActive(true);
            staffGachaPanel.SetActive(false);
            staffChoicePanelObj.SetActive(true);
            clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
            {
                clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
                SceneManager.LoadScene("SceneHaewoong");
            });
        }
        else
        {
            ShowExplane("3�� �̻��� �������� �������ּ���.");   
        }
    }

    public void ShowExplane(string showText)
    {
        explaneText.gameObject.SetActive(true);
        explaneText.text = showText;
        explaneText.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
        {
            explaneText.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            //isOneClick = false;
            //explaneText.gameObject.SetActive(false);
        });

    }

    public void MakeMusicStart()
    {
        staffGachaPanel.SetActive(false);
        clearPanel.SetActive(true);
        clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
        {
            clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            isOneClick = false;
            companyPanel.SetActive(false);
            staffChoicePanelObj.SetActive(true);
        });
    }

    public void GachaGradeStart()
    {
        gachaGradePanel.SetActive(true);

        gradeButtons[0].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[0]); minusMoney = 500; });
        gradeButtons[1].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[1]); minusMoney = 1000; });
        gradeButtons[2].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[2]); minusMoney = 5000; }); 
    }

    public void RealChoiceQuestion(Button showButton)
    {
            string showText = showButton.transform.GetChild(0).GetComponent<Text>().text;
            realPanelObj.transform.GetChild(1).GetComponent<Text>().text = showText;
            realPanelObj.SetActive(true);
    }

    public void YesGacha()
    {
        if (GameManager.instance.playerMoney >= minusMoney)
        {
            GameManager.instance.playerMoney -= minusMoney;
            realPanelObj.SetActive(false);
            gachaGradePanel.SetActive(false);

            clearPanel.SetActive(true);
            clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
            {
                clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
                isOneClick = false;
                StaffGachaStart();
                minusMoney = 0;
                companyPanel.SetActive(false);
                staffGachaPanel.SetActive(true);
            });
            staffChoicePanelObj.SetActive(false);
        }
        else
        {
            ShowExplane("���� �����մϴ�.");
        }
    }

    public void NoGacha()
    {
        realPanelObj.SetActive(false);
    }


    public void SelectPanelClick()
    {
        selectPanel.SetActive(true);

        Transform[] childList = staffListPanel.GetComponentsInChildren<Transform>();
        if (staffListPanel.transform.childCount != 0)
        {
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform)
                    {
                        Destroy(childList[i].gameObject);
                    }
                }
            }
        }
        GameObject noneStaff = Instantiate(staffPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        noneStaff.transform.parent = staffListPanel.transform;
        noneStaff.transform.localScale = new Vector3(1, 1, 1);
        noneStaff.GetComponent<Image>().sprite = Resources.Load<Sprite>("delete");
        Destroy(noneStaff.GetComponent<StaffData>());
        Destroy(noneStaff.GetComponent<ExplaneButton>()); 
        noChoiceStaffButton = noneStaff.GetComponent<Button>();
        noChoiceStaffButton.onClick.AddListener(() => { StaffNoChoice(); });

        for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
            {
            GameObject staff = Instantiate(staffPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            staff.transform.parent = staffListPanel.transform;
            staff.transform.localScale = new Vector3(1, 1, 1);  

            if (!isSort)
            {
                StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                {
                    if (a.StaffNumber > b.StaffNumber) return 1;
                    else if (a.StaffNumber < b.StaffNumber) return -1;
                    return 0;
                });
            }

            staff.GetComponent<Image>().sprite = StaffManager.instance.workStaffList[i].MySprite;
            staff.GetComponent<StaffData>().myStaffData = StaffManager.instance.workStaffList[i];

            if (buttons[buttonCount - 1].GetComponent<Image>().sprite == null)
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { SelectWorkStaff(staff); });
            }
            else
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { DistinctSelectWorkStaff(staff); });
            }
        }

    } 

    public void SelectWorkStaff(GameObject staffObj)
    {
        StaffManager.instance.pickWorkStaffList.Add(staffObj.GetComponent<StaffData>().myStaffData);
        StaffManager.instance.workStaffList.Remove(staffObj.GetComponent<StaffData>().myStaffData);
        buttons[buttonCount - 1].GetComponent<Image>().sprite = staffObj.GetComponent<Image>().sprite;

        memberCount++;
        StatSetting();
        selectPanel.SetActive(false);
        sortText.text = ""; 
        explanePanel.SetActive(false);
    }

    public void StatReset()
    {
        GameManager.instance.allCreativity = 0;
        GameManager.instance.allAddictive = 0;
        GameManager.instance.allMelodic = 0;
        GameManager.instance.allPopularity = 0;
    }

    public void ShowStat()
    {
        statTexts[0].text = GameManager.instance.allCreativity.ToString();
        statTexts[1].text = GameManager.instance.allAddictive.ToString();
        statTexts[2].text = GameManager.instance.allMelodic.ToString();
        statTexts[3].text = GameManager.instance.allPopularity.ToString();
    }

    public void StatSetting()
    {
        StatReset();
        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            GameManager.instance.allCreativity += StaffManager.instance.pickWorkStaffList[i].Creativity;
            GameManager.instance.allAddictive += StaffManager.instance.pickWorkStaffList[i].Addictive;
            GameManager.instance.allMelodic += StaffManager.instance.pickWorkStaffList[i].Melodic;
            GameManager.instance.allPopularity += StaffManager.instance.pickWorkStaffList[i].Popularity;
        }
        ShowStat();
    }

    public void DistinctSelectWorkStaff(GameObject staffObj) //�̹� �ִ� �������� �����Ͽ�����
    {
        for (int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if (buttons[buttonCount - 1].GetComponent<Image>().sprite == StaffManager.instance.pickWorkStaffList[i].MySprite)
            {
                StatSetting(); 
                StaffManager.instance.workStaffList.Add(StaffManager.instance.pickWorkStaffList[i]);
                StaffManager.instance.pickWorkStaffList.Remove(StaffManager.instance.pickWorkStaffList[i]);
            }
        }
        StaffManager.instance.pickWorkStaffList.Add(staffObj.GetComponent<StaffData>().myStaffData);
        StaffManager.instance.workStaffList.Remove(staffObj.GetComponent<StaffData>().myStaffData);
        buttons[buttonCount - 1].GetComponent<Image>().sprite = staffObj.GetComponent<Image>().sprite;

        selectPanel.SetActive(false);
        sortText.text = "";
        explanePanel.SetActive(false);
    }

    public void StaffNoChoice()
    {
        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if(buttons[buttonCount - 1].GetComponent<Image>().sprite == StaffManager.instance.pickWorkStaffList[i].MySprite)
            {
                GameManager.instance.allAddictive -= StaffManager.instance.pickWorkStaffList[i].Addictive;
                GameManager.instance.allCreativity -= StaffManager.instance.pickWorkStaffList[i].Creativity;
                GameManager.instance.allMelodic -= StaffManager.instance.pickWorkStaffList[i].Melodic;
                GameManager.instance.allPopularity -= StaffManager.instance.pickWorkStaffList[i].Popularity;
                StaffManager.instance.workStaffList.Add(StaffManager.instance.pickWorkStaffList[i]);
                StaffManager.instance.pickWorkStaffList.Remove(StaffManager.instance.pickWorkStaffList[i]);
            }
        }
        if(buttons[buttonCount - 1])
        buttons[buttonCount - 1].GetComponent<Image>().sprite = null;
        if (memberCount >= 1)
        {
            memberCount--;
        }
        selectPanel.SetActive(false);
        explanePanel.SetActive(false);
        sortText.text = "";
        ShowStat();
    }

    public void PickUpStaffEnd()
    {
        StatReset();
        ShowStat(); 
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            StaffManager.instance.workStaffList.Add(StaffManager.instance.pickWorkStaffList[i]);
        }
        StaffManager.instance.pickWorkStaffList.Clear();
        ClearTween(staffChoicePanelObj);
        //staffChoicePanelObj.SetActive(false);
    }

    public void ClosePickUpPanel()
    {
        if(selectPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                selectPanel.SetActive(false);
                explanePanel.SetActive(false);
                sortText.text = "";
            }
        }
        else if(gachaGradePanel.activeSelf)
        {
            if (!realPanelObj.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gachaGradePanel.SetActive(false);
                }
            }
        }
    }


    public void TestPanel()
    {
        moneyText.text = "������ : " + GameManager.instance.playerMoney.ToString() + "G";  
        if(Input.GetKeyDown(KeyCode.C))
        {
            companyPanel.SetActive(false);
            staffGachaPanel.SetActive(true);
            StaffGachaStart();
            staffChoicePanelObj.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            companyPanel.SetActive(false); 
            staffGachaPanel.SetActive(false);
            staffChoicePanelObj.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StaffGachaStart(); 
        }
    }       

    public void StaffSort(GameObject sortObj, GameObject panelObj)
    {
        Debug.Log(53);
        isSort = true;
        switch (sortObj.GetComponent<SortButtonData>().sortData)
        {
            case SortButtonData.Sort.creativity:
                Debug.Log(1);
                for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
                {
                    StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                    {
                        if (a.Creativity < b.Creativity) return 1;
                        else if (a.Creativity > b.Creativity) return -1;
                        return 0;
                    });
                }
                sortText.text = sortObj.transform.GetChild(0).GetComponent<Text>().text;

                break;
            case SortButtonData.Sort.addictive:
                Debug.Log(2);
                for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
                {
                    StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                {
                    if (a.Addictive < b.Addictive) return 1;
                    else if (a.Addictive > b.Addictive) return -1;
                    return 0;
                });
                }
                sortText.text = sortObj.transform.GetChild(0).GetComponent<Text>().text;
                break;
            case SortButtonData.Sort.melodic:
                Debug.Log(3);
                for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
                {
                    StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                {
                    if (a.Melodic < b.Melodic) return 1;
                    else if (a.Melodic > b.Melodic) return -1;
                    return 0;
                });
                }
                sortText.text = sortObj.transform.GetChild(0).GetComponent<Text>().text;
                break;
            case SortButtonData.Sort.popularity: 
                Debug.Log(4);
                for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
                {
                    StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                {
                    if (a.Popularity < b.Popularity) return 1;
                    else if (a.Popularity > b.Popularity) return -1;
                    return 0;
                });
                }
                sortText.text = sortObj.transform.GetChild(0).GetComponent<Text>().text;
                break;
            case SortButtonData.Sort.level:
                Debug.Log(4);
                for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
                {
                    StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
                    {
                        if (a.StaffLevel < b.StaffLevel) return 1;
                        else if (a.StaffLevel > b.StaffLevel) return -1; 
                        return 0;
                    });
                }
                sortText.text = sortObj.transform.GetChild(0).GetComponent<Text>().text;
                break;
            default:
                break;
        }
        for(int i = 1; i < staffListPanel.transform.childCount; i++)
        {
            Destroy(staffListPanel.transform.GetChild(i).gameObject);
        } 
        SelectPanelClick();
        isSort = false; 
        panelObj.SetActive(false); 

    }
}
