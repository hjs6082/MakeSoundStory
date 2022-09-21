using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{                   
    public static UIManager instance;

    private readonly string[] STAFF_INFO_STRS = 
    {
        "??? : ",
        "???? : ",
        "????? : ",
        "??��??? : ",
        "????? : ",
        "????? : ",
        "??????? ?? : ",
        "?????? ?? : ",
        "???? : ",
        "???? : "
    };
#region Values
    public bool isOneClick = false; // ????? ??? ??? bool ????? ??)?????? ?????? ?????????? ?????? ???????? 1??????? ?��? ????.

    private bool isSort = false; //Sort?? ??��????? ???? bool;

    public GameObject staffTalkPanel = null;
    public GameObject staffStatus = null;

#region NPC Panel
    [Header("NPC Panel")]
    public GameObject staffNpcPanel = null;
    public GameObject musicNpcPanel = null;
    public GameObject settingNpcPanel = null;
    public GameObject bankNpcPanel = null;
    public GameObject officeNpcPanel = null;
    public GameObject shopNpcPanel = null;
#endregion

    public Text staffTalkText = null;
    public Text staffTalkNameText = null;

    public Sprite talkImage = null;

    public Transform staffImage = null;

    [SerializeField] private Transform staffSpawnPosition = null;

    [SerializeField] private GameObject explanePanel     = null; // ?��???? ???�J?? ??????? ?????? ???? ???? ?��?.
    [SerializeField] private GameObject buttonPanel      = null; // ??????? ????? ?��?
    [SerializeField] private GameObject genrePanel       = null; 
    [SerializeField] private GameObject genreChoicePanel = null; 
    [SerializeField] private GameObject companyPanel     = null; //??? ?��? 
    [SerializeField] private Text       explaneText      = null; //???? ???? ??) ???????? ????????.
    [SerializeField] private Text       moneyText        = null; // ?????? ????
    [SerializeField] private Text       mainText         = null; // ???? ????
    [SerializeField] private Text       calendarText     = null; // ??? ????
    [SerializeField] private Button     genreButton      = null;
    [SerializeField] private Transform  genreTransform   = null; // ???? ????

    private int minusMoney = 0;

    //---------?????? ??? ???? ????-----------
    [SerializeField] private Button         gachaStartButton = null; //??? ???? ???
    [SerializeField] private GameObject     gachaGradePanel  = null; //????? ????? ????? ?��?
    [SerializeField] private Button[]       gradeButtons     = null;
    [SerializeField] private GameObject     staffPanelObj    = null;   //?????? ?��??? ??????? ????????? ?��? ???????
    [SerializeField] private GameObject     realPanelObj     = null;   //?????? ???????? ?????? ???????
    [SerializeField] private GameObject[]   staffPanels      = null; //?? 3???? ???????? ??? ?�ԥ�?;
    [SerializeField] private GameObject     gradeSelectPanel = null;
    [SerializeField] private GameObject     clearPanel       = null; //???? ??? ?????? ?��?

    public GameObject staffGachaPanel; //?????? ??? ?��?

    //---------?????? ???? ???? ????-----------
    public GameObject staffChoicePanelObj; // ?????? ???? ????��?
    
    public GameObject selectPanel; // ???????? ??????? ?��?

    [SerializeField] private GameObject staffPrefab         = null; // ???????? ????? ?��?? ?????? ?????????? ??????
    [SerializeField] private GameObject staffListPanel      = null; //???????? ??????? ??????? ?��?
    [SerializeField] private Button[]   buttons             = null; //????? ?????? ?? ??? ?????
    [SerializeField] private Button     pickUpExitButton    = null; //?????? ???
    [SerializeField] private Button     makeMusicButton     = null; //???? ????? ???
    [SerializeField] private Button     choiceStaffButton   = null; //?????? ???? ???
    [SerializeField] private Button     noChoiceStaffButton = null; //?????? ???? ???? ???
    [SerializeField] private Button[]   sortButtons         = null;
    [SerializeField] private Text[]     statTexts           = null;
    [SerializeField] private Text       sortText            = null;

    //------------------?????? ??? ???? ????-----------------
    [Header("Show Staff")]
    [SerializeField] private Button     showStaffListButton      = null;
    [SerializeField] private Button     showStaffListLeftButton  = null;
    [SerializeField] private Button     showStaffListRightButton = null;
    [SerializeField] private GameObject showStaffListPanel       = null;
    [SerializeField] private Text       showStaffPageText        = null;
    [SerializeField] private int        showStaffCount = 0;

    //-----------------?????? ??? ???? ???? ------------------
    [SerializeField] private Button staffSpawnButton          = null;  //?????? ??? ???    
    [SerializeField] private GameObject staffSpawnPanel       = null;  //?????? ??? ?��? ???????
    [SerializeField] private GameObject spawnStaffChoicePanel = null; // ?????? ?????? ?��?
    [SerializeField] private GameObject spawnStaffChoiceListPanel = null; // ?????? ?????? ?��?

    [SerializeField] private Button musicMakeStartButton = null;

    // -----------------???? ???? ???? ------------------
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private GameObject oxPanel;
    private Vector3 eventStartPosition;

    [SerializeField] private GameObject dogamPanel = null;

    private string[] sayList = { "?????? ?? ????��???!", "?? ??�� ???��? ??? ?????!","?????? ?????? ?????????!" };

    public int buttonCount = 0;
    private int staffCount = 0;

    [SerializeField] private int memberCount = 0;

    private List<GameObject> gachaStaff = new List<GameObject>();
    private GameObject explainStaffUnit = null;
    private GameObject showStaffUnit = null;
#endregion

#region 기본 메서드
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("??? UI??????? ??????.");
        }
        else
        {
            instance = this;
        }
        InitButton();
        eventStartPosition = eventPanel.transform.position;
    }

    void Update()
    {
        ClosePickUpPanel();
        TestPanel();
        Explane();
    }
#endregion

#region RandomStaff
    public void StaffGatcha(StaffSO selectStaff,int index)
    {
        StaffManager.instance.staffList.RemoveAt(index);
        //Debug.Log("????");
        int staffPanelsCount = staffPanelObj.transform.childCount;
        
        staffPanels = new GameObject[staffPanelsCount];
        for(int i = 0; i < staffPanels.Length/* - 1*/; i++)
        {
            staffPanels[i] = staffPanelObj.transform.GetChild(i).gameObject;
        }

        StaffManager.instance.staffList.Remove(selectStaff);
        GameObject staffUnit = InitStaffInfo(staffPanels[staffCount], selectStaff, 11);
        gachaStaff.Add(staffUnit);
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

    public void StaffGachaStart() {
        Debug.Log(23);
        companyPanel.SetActive(false);
        buttonPanel.SetActive(false); 
        staffGachaPanel.SetActive(true);

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

    public void StaffGachaEnd() {
        for(int i = 0; i < staffPanels.Length; i++)
        {
            Image image = staffPanels[i].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.42f;
            image.color = tempColor;
        }
        StaffManager.instance.isSelectStaff = 0; // ???? -1?????.
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
                buttonPanel.SetActive(false);
                companyPanel.SetActive(false);
                staffGachaPanel.SetActive(true);
            });
            staffChoicePanelObj.SetActive(false);
        }
        else
        {
            ShowExplane("???? ????????.");
        }
    }

    public void NoGacha()
    {
        realPanelObj.SetActive(false);
    }

    public void GachaGradeStart()
    {
        gachaGradePanel.SetActive(true);

        gradeButtons[0].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[0]); minusMoney = 500; });
        gradeButtons[1].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[1]); minusMoney = 1000; });
        gradeButtons[2].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[2]); minusMoney = 5000; }); 
    }
#endregion

#region Staff Select
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
            ShowExplane("???? ????????.");
        }
        staffPanel.SetActive(true);
        staffPanel.transform.DOScale(new Vector3(1.2f, 1.2f), 1.3f).OnComplete(() =>
        {
            ClearTween(staffGachaPanel);
            staffPanel.GetComponent<MyData>().isSelect = false;
            //GameObject staff = Instantiate(staffPanel.GetComponent<MyData>().myStaff.StaffPrefab, staffSpawnPosition.position, Quaternion.identity);
            //staff.transform.parent = staffSpawnPosition.transform;
            //staff.transform.localScale = new Vector3(1f, 1f, 1f);
            NPC.NPC_Management.Instance.AddNPC(staffPanel.GetComponent<MyData>().myStaff);
            int randomIndex = UnityEngine.Random.Range(0, sayList.Length);
            HumanEvent(staffPanel.GetComponent<MyData>().myStaff, sayList[randomIndex], false);
            Dogam.instance.DogamChange(staffPanel.GetComponent<MyData>().myStaff.StaffNumber);
        });
    }

    
#endregion

#region Show Staff
    public void showStaffLeft()
    {
        if(showStaffCount >= 1)
        {
            Destroy(showStaffUnit);
            showStaffCount--;  
            int listIndex = showStaffCount + 1;
            StaffSO staffSO = StaffManager.instance.workStaffList[showStaffCount];
            showStaffUnit = InitStaffInfo(showStaffListPanel, staffSO, 9);
            showStaffPageText.text = listIndex + "/" + StaffManager.instance.workStaffList.Count;
        }
    }


    public void showStaffRight()
    {
        if (showStaffCount <= StaffManager.instance.workStaffList.Count -2)
        {
            Destroy(showStaffUnit);
            showStaffCount++;
            int listIndex = showStaffCount + 1;
            StaffSO staffSO = StaffManager.instance.workStaffList[showStaffCount];
            showStaffUnit = InitStaffInfo(showStaffListPanel, staffSO, 9);

            showStaffPageText.text = listIndex + "/" + StaffManager.instance.workStaffList.Count;
        }
    }

    public void showStaffList()
    {
        if (StaffManager.instance.workStaffList.Count != 0)
        {
            int listIndex = showStaffCount + 1;
            showStaffListPanel.SetActive(true);

            Destroy(showStaffUnit);
            showStaffUnit = InitStaffInfo(showStaffListPanel, StaffManager.instance.workStaffList[showStaffCount], 9);
            showStaffPageText.text = listIndex + "/" + StaffManager.instance.workStaffList.Count;
        }
        else
        {
            ShowExplane("?????? ???????.");
        }
    }
#endregion

    
    public void InitButton()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => { buttonCount = i + 1; SelectPanelClick(); });
        }

        pickUpExitButton.onClick.AddListener(() => { PickUpStaffEnd(); });
        //choiceStaffButton.onClick.AddListener(() => { MusicSceneChange(); /*ClearTween(staffChoicePanelObj);*/ });
        
        // gachaStartButton.onClick.AddListener(() => { GachaGradeStart(); });
        // makeMusicButton.onClick.AddListener(() => { MakeMusicStart(); });
        showStaffListButton.onClick.AddListener(() => { showStaffList(); });
        showStaffListLeftButton.onClick.AddListener(() => { showStaffLeft(); });
        showStaffListRightButton.onClick.AddListener(() => { showStaffRight(); });
        genreButton.onClick.AddListener(() => { genreChoicePanel.SetActive(true);
                                                GenreManager.instance.GenreSet(genreTransform); });
        //staffSpawnButton.onClick.AddListener(() => { staffSpawnPanel.SetActive(true); });
        musicMakeStartButton.onClick.AddListener(() =>
        {
            GameManager.instance.curBPM = BPM_Management.Instance.curBPM;
        });
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
        explainStaffUnit = InitStaffInfo(explanePanel, staffSO, 7); 
    }

    public void ExplaneExit()
    {
        Destroy(explainStaffUnit);
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

    

    public void CalendarSetting()
    {
        calendarText.text = GameManager.instance.month + "?? " + GameManager.instance.day + "??";
    }

    
     
    
    public void ClearTween(GameObject falsePanel)
    {
        clearPanel.SetActive(true);
        clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                Destroy(gachaStaff[i]);
            }
            gachaStaff.Clear();
                
            
            falsePanel.SetActive(false);
            clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            isOneClick = false;
            StaffGachaEnd();
            companyPanel.SetActive(true);
            buttonPanel.SetActive(true);
        });
    }

    



    //---------?????? ???? ???? ???-----------

    

    private GameObject InitStaffInfo(GameObject _infoPanel, StaffSO _staffSO, int _childCount)
    {
        string info = string.Empty;
        StringBuilder sb = new StringBuilder();
        Transform infoTrm = _infoPanel.transform;
        Vector3 unitOffset = Vector3.down * 0.5f;

        if(infoTrm.childCount > _childCount)
        {
            for(int i = _childCount; i < infoTrm.childCount; i++)
            {
                Destroy(infoTrm.GetChild(i).gameObject);
            }
        }

        GameObject staffUnit = Instantiate(_staffSO.StaffPrefab, infoTrm.GetChild(0).position + unitOffset, Quaternion.identity, infoTrm);
        staffUnit.GetComponent<RectTransform>().localScale = new Vector2(150, 150);
        
        if(staffUnit.GetComponent<SortingGroup>() != null)
        {
            staffUnit.GetComponent<SortingGroup>().sortingOrder = 50;
        }
        else
        {
            staffUnit.AddComponent<SortingGroup>().sortingOrder = 50;
        }

        return staffUnit;
    }

    public void showDogam()
    {
        dogamPanel.SetActive(true);
        //Camera.main.GetComponent<CameraSetting>().enabled = false;
        dogamPanel.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => { dogamPanel.SetActive(false);
            //Camera.main.GetComponent<CameraSetting>().enabled = true;
        });
    }

    public void MusicSceneChange()
    {
        if (memberCount >= 3)
        {
            companyPanel.SetActive(false);
            staffGachaPanel.SetActive(false);
            staffChoicePanelObj.SetActive(false); 
            mainText.gameObject.SetActive(false);
            clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
            {
                clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
                genrePanel.SetActive(true);
            });
        }
        else
        {
            ShowExplane("3?? ????? ???????? ???????????.");   
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
            buttonPanel.SetActive(false);
            companyPanel.SetActive(false);
            staffChoicePanelObj.SetActive(true);
        });
    }

    public void GachaGradeEnd()
    {
        gachaGradePanel.SetActive(false);
    }

    public void RealChoiceQuestion(Button showButton)
    {
            string showText = showButton.transform.GetChild(0).GetComponent<Text>().text;
            realPanelObj.transform.GetChild(1).GetComponent<Text>().text = showText;
            realPanelObj.SetActive(true);
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
        noneStaff.transform.SetParent(staffListPanel.transform);
        noneStaff.transform.localScale = new Vector3(1, 1, 1);
        noneStaff.GetComponent<Image>().sprite = Resources.Load<Sprite>("delete");
        Destroy(noneStaff.GetComponent<StaffData>());
        Destroy(noneStaff.GetComponent<ExplaneButton>()); 
        noChoiceStaffButton = noneStaff.GetComponent<Button>();
        noChoiceStaffButton.onClick.AddListener(() => { StaffNoChoice(); });

        for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
        {
            GameObject staff = Instantiate(staffPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            staff.transform.SetParent(staffListPanel.transform);
            staff.transform.localScale = new Vector3(1, 1, 1);  

            if (!isSort)
            {
                StaffManager.instance.workStaffList.Sort((StaffSO a, StaffSO b) =>
                {
                    if (a.StaffNumber > b.StaffNumber) return 1;
                    else if (a.StaffNumber < b.StaffNumber) return -1;
                    return 0;
                });
            }

            GameObject staffUnit = Instantiate(StaffManager.instance.workStaffList[i].StaffPrefab, staff.GetComponent<Image>().transform.position + Vector3.down * 0.6f, Quaternion.identity, staff.GetComponent<Image>().transform);
            staffUnit.GetComponent<RectTransform>().localScale = new Vector3(125, 125, 1);
            staff.GetComponent<StaffData>().myStaffData = StaffManager.instance.workStaffList[i];

            if(staffUnit.GetComponent<SortingGroup>() != null)
            {
                staffUnit.GetComponent<SortingGroup>().sortingOrder = 30;
            }
            else
            {
                staffUnit.AddComponent<SortingGroup>().sortingOrder = 30;
            }

            if (buttons[buttonCount - 1].GetComponent<Image>().sprite == null)
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { SelectWorkStaff(staff, staffUnit); });
            }
            else
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { DistinctSelectWorkStaff(staff, staffUnit); });
            }
        }

    } 

    public void SelectWorkStaff(GameObject staffObj, GameObject staffUnit)
    {
        StaffManager.instance.pickWorkStaffList.Add(staffObj.GetComponent<StaffData>().myStaffData);
        StaffManager.instance.workStaffList.Remove(staffObj.GetComponent<StaffData>().myStaffData);
        GameObject buttonUnit = Instantiate(staffUnit, buttons[buttonCount - 1].transform.position + Vector3.down * 0.5f, Quaternion.identity, buttons[buttonCount - 1].transform);

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

    public void DistinctSelectWorkStaff(GameObject staffObj, GameObject staffUnit) //??? ??? ???????? ???????????
    {
        for (int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if (buttons[buttonCount - 1].GetComponent<Image>().sprite == StaffManager.instance.pickWorkStaffList[i].StaffPrefab)
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
            if(buttons[buttonCount - 1].GetComponent<Image>().sprite == StaffManager.instance.pickWorkStaffList[i].StaffPrefab)
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
        clearPanel.SetActive(true);
        clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() =>
        {
            clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            isOneClick = false;
            buttonPanel.SetActive(true);
            staffChoicePanelObj.SetActive(false);
            clearPanel.SetActive(false);
        });
        companyPanel.SetActive(true);
    }

    public void ClosePickUpPanel()
    {
        if(selectPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                selectPanel.SetActive(false);
                explanePanel.SetActive(false);
            }
        }
        else if(gachaGradePanel.activeSelf)
        {
            if (!realPanelObj.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GachaGradeEnd();
                }
            }
        }
        else if(showStaffListPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                showStaffListPanel.SetActive(false);
            }
        }
    }

    public void TestPanel()
    {
        moneyText.text = "?????? : " + GameManager.instance.playerMoney.ToString() + "G";  
        if(Input.GetKeyDown(KeyCode.C))
        {
            buttonPanel.SetActive(false);
            companyPanel.SetActive(false);
            staffGachaPanel.SetActive(true);
            StaffGachaStart();
            staffChoicePanelObj.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            buttonPanel.SetActive(false);
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

    public void SpawnStaff()
    {
        spawnStaffChoicePanel.SetActive(true);

        Transform[] childList = spawnStaffChoiceListPanel.GetComponentsInChildren<Transform>();
        if (spawnStaffChoiceListPanel.transform.childCount != 0)
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
        noneStaff.transform.parent = spawnStaffChoiceListPanel.transform;
        noneStaff.transform.localScale = new Vector3(1, 1, 1);
        noneStaff.GetComponent<Image>().sprite = Resources.Load<Sprite>("delete");
        Destroy(noneStaff.GetComponent<StaffData>());
        Destroy(noneStaff.GetComponent<ExplaneButton>());
        //noChoiceStaffButton = noneStaff.GetComponent<Button>();
        //noChoiceStaffButton.onClick.AddListener(() => { StaffNoChoice(); });

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

            GameObject staffUnit = Instantiate(StaffManager.instance.workStaffList[i].StaffPrefab, staff.GetComponent<Image>().transform.position + Vector3.down * 0.6f, Quaternion.identity, staff.GetComponent<Image>().transform);
            staffUnit.GetComponent<RectTransform>().localScale = new Vector3(150, 150, 1);
            staff.GetComponent<StaffData>().myStaffData = StaffManager.instance.workStaffList[i];

            // if (buttons[buttonCount - 1].GetComponent<Image>().sprite == null)
            // {
            //     staff.GetComponent<Button>().onClick.AddListener(() => { SelectWorkStaff(staff, staffUnit); });
            // }
            // else
            // {
            //     staff.GetComponent<Button>().onClick.AddListener(() => { DistinctSelectWorkStaff(staff, staffUnit); });
            // }
        }
    }

    public void notHumanEvent(string text, bool isOX)
    {
        if (isOX)
        {
            oxPanel.SetActive(true);
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
    }

    public void HumanEvent(StaffSO staff, string text, bool isOX)
    {

        eventPanel.transform.position = eventStartPosition;
        if (isOX)
        {
            if (eventPanel.transform.GetChild(0).gameObject.transform.childCount != 0)
            {
                Destroy(eventPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
            }
            oxPanel.SetActive(true);
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            GameObject setStaff = Instantiate(staff.StaffPrefab, eventPanel.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
            setStaff.transform.parent = eventPanel.transform.GetChild(0);
            setStaff.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 31;
            //setStaff.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 31;
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
        if (!isOX)
        {
            if(eventPanel.transform.GetChild(0).gameObject.transform.childCount !=0)
            {
                Destroy(eventPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
            }
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            GameObject setStaff = Instantiate(staff.StaffPrefab, eventPanel.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
            setStaff.transform.SetParent(eventPanel.transform.GetChild(0));
            //setStaff.transform.parent = eventPanel.transform.GetChild(0);
            setStaff.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 31;
            //setStaff.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 31;
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
    }

    IEnumerator endEvent(float time, GameObject falseObj)
    {
        yield return new WaitForSeconds(time);
        falseObj.SetActive(false);
    }

}
