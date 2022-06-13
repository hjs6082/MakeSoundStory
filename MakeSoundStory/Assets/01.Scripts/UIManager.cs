using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool isOneClick = false; // 한번만 되게 하는 bool 함수임 예)스태프 선택이 누를때마다 들어오면 안되기때문에 1회제한을 두는 변수.

    private bool isSort = false; //Sort를 하는것인지 체크용 bool;



    [SerializeField]
    private GameObject companyPanel; //회사 패널 

    //---------스태프 뽑기 관련 변수-----------

    [SerializeField]
    private GameObject staffPanelObj;   //스태프 패널을 자식으로 가지고있는 부모 오브젝트

    [SerializeField]
    private GameObject[] staffPanels; //각 3개의 스태프를 띄울 패널들;

    [SerializeField]
    private GameObject clearPanel; //종료때 띄울 검은색 패널

    public GameObject staffGachaPanel; //스태프 가챠 패널

    //---------스태프 선택 관련 변수-----------
    public GameObject staffChoicePanelObj; // 스태프 선택 전체패널
    
    public GameObject selectPanel; // 스태프를 선택하는 패널

    [SerializeField]
    private GameObject staffPrefab; // 스태프를 추가할 용도로 사용하는 게임오브젝트 프리팹

    [SerializeField]
    private GameObject staffListPanel; //스태프를 자식으로 가지게될 패널

    [SerializeField]
    private Button[] buttons; //멤버를 선택할 수 있는 버튼들

    [SerializeField]
    private Button pickUpExitButton; //나가기 버튼

    [SerializeField]
    private Button choiceStaffButton; //스태프 선택 버튼

    [SerializeField]
    private Button noChoiceStaffButton; //스태프 선택 안함 버튼

    [SerializeField]
    private Button[] sortButtons;

    [SerializeField]
    private Text[] statTexts;

    [SerializeField]
    private Text sortText;

    public int buttonCount = 0;

    private int staffCount = 0;

    void Start()
    {
        if (instance != null)
        {
            Debug.Log("이미 UI매니저가 있습니다.");
        }
        else
        {
            instance = this;
        }
        StaffGachaStart();
        ButtonClick();
    }

    void Update()
    {
        ClosePickUpPanel();
        TestPanel();
    }

    public void StaffGatcha(StaffSO selectStaff)
    {
        //Debug.Log("시작");
        int staffPanelsCount = staffPanelObj.transform.childCount;
        
        staffPanels = new GameObject[staffPanelsCount];
        for(int i = 0; i < staffPanels.Length; i++)
        {
            staffPanels[i] = staffPanelObj.transform.GetChild(i).gameObject;
        }

        StaffManager.instance.staffList.Remove(selectStaff);
        staffPanels[staffCount].transform.GetChild(0).GetComponent<Image>().sprite = selectStaff.MySprite;
        staffPanels[staffCount].transform.GetChild(1).GetComponent<Text>().text = "이름 : " + selectStaff.StaffName;
        staffPanels[staffCount].transform.GetChild(2).GetComponent<Text>().text = "직업 : " + selectStaff.StaffJob;
        staffPanels[staffCount].transform.GetChild(3).GetComponent<Text>().text = "좋아하는 장르 : " + selectStaff.FavoriteGenre;
        staffPanels[staffCount].transform.GetChild(4).GetComponent<Text>().text = "싫어하는 장르 : " + selectStaff.HateGenre;
        staffPanels[staffCount].transform.GetChild(6).GetComponent<Text>().text = "독창성 : " + selectStaff.Creativity;
        staffPanels[staffCount].transform.GetChild(7).GetComponent<Text>().text = "멜로디컬 : " + selectStaff.Melodic;
        staffPanels[staffCount].transform.GetChild(8).GetComponent<Text>().text = "중독성 : " + selectStaff.Addictive;
        staffPanels[staffCount].transform.GetChild(9).GetComponent<Text>().text = "대중성 : " + selectStaff.Popularity;
        staffPanels[staffCount].transform.GetChild(10).GetComponent<Text>().text = "계약금 : " + selectStaff.Money + "G";
        staffPanels[staffCount].transform.GetChild(12).GetComponent<Text>().text = "레벨 : " + selectStaff.StaffLevel; 
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
        StaffManager.instance.isSelectStaff--; 
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



    //---------스태프 선택 관련 함수-----------

    public void ButtonClick()
    {
        buttons[0].onClick.AddListener(() => { buttonCount = 1; SelectPanelClick(); });
        buttons[1].onClick.AddListener(() => { buttonCount = 2; SelectPanelClick(); });
        buttons[2].onClick.AddListener(() => { buttonCount = 3; SelectPanelClick(); });
        buttons[3].onClick.AddListener(() => { buttonCount = 4; SelectPanelClick(); });
        buttons[4].onClick.AddListener(() => { buttonCount = 5; SelectPanelClick(); });

        pickUpExitButton.onClick.AddListener(() => { PickUpStaffEnd(); });
        choiceStaffButton.onClick.AddListener(() => { ClearTween(staffChoicePanelObj); });
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

        StatSetting();
        selectPanel.SetActive(false);
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

    public void DistinctSelectWorkStaff(GameObject staffObj) //이미 있는 스태프를 수정하였을때
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
        selectPanel.SetActive(false);
        ShowStat();
    }

    public void PickUpStaffEnd()
    {
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
            }
        }
    }

    public void TestPanel()
    {
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
