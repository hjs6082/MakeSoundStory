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

    public bool isOneClick = false; // 한번만 되게 하는 bool 함수임 예)스태프 선택이 누를때마다 들어오면 안되기때문에 1회제한을 두는 변수.

    private bool isSort = false; //Sort를 하는것인지 체크용 bool;

    public GameObject staffTalkPanel = null;
    public GameObject staffStatus = null;

    public GameObject staffNpcPanel = null;
    public GameObject musicNpcPanel = null;
    public GameObject settingNpcPanel = null;
    public GameObject bankNpcPanel = null;
    public GameObject officeNpcPanel = null;
    public GameObject shopNpcPanel = null;

    public Text staffTalkText = null;
    public Text staffTalkNameText = null;

    public Sprite talkImage = null;

    public Transform staffImage = null;

    [SerializeField] private Transform staffSpawnPosition = null;

    [SerializeField] private GameObject explanePanel     = null; // 플레이어 마우스를 따라다니며 설명을 해줄 설명 패널.
    [SerializeField] private GameObject buttonPanel      = null; // 버튼들이 모여있는 패널
    [SerializeField] private GameObject genrePanel       = null; 
    [SerializeField] private GameObject genreChoicePanel = null; 
    [SerializeField] private GameObject companyPanel     = null; //회사 패널 
    [SerializeField] private Text       explaneText      = null; //설명 텍스트 예) 소지금이 부족합니다.
    [SerializeField] private Text       moneyText        = null; // 소지금 텍스트
    [SerializeField] private Text       starText         = null; // 회사 몇성인지 텍스트
    [SerializeField] private Text       staffCountText   = null; // 스태프 몇명인가 텍스트
    [SerializeField] private Text       mainText         = null; // 메인 텍스트
    [SerializeField] private Text       calendarText     = null; // 달력 텍스트
    [SerializeField] private Button     genreButton      = null;
    [SerializeField] private Transform  genreTransform   = null; // 메인 텍스트

    private int minusMoney = 0;

    //---------스태프 뽑기 관련 변수-----------
    [SerializeField] private Button         gachaStartButton = null; //가챠 스타트 버튼
    [SerializeField] private GameObject     gachaGradePanel  = null; //가챠의 등급을 정하는 패널
    [SerializeField] private Button[]       gradeButtons     = null;
    [SerializeField] private GameObject     staffPanelObj    = null;   //스태프 패널을 자식으로 가지고있는 부모 오브젝트
    [SerializeField] private GameObject     realPanelObj     = null;   //정말로 고를거냐고 물어보는 오브젝트
    [SerializeField] private GameObject[]   staffPanels      = null; //각 3개의 스태프를 띄울 패널들;
    [SerializeField] private GameObject     gradeSelectPanel = null;
    [SerializeField] private GameObject     clearPanel       = null; //종료때 띄울 검은색 패널

    public GameObject staffGachaPanel; //스태프 가챠 패널

    //---------스태프 선택 관련 변수-----------
    public GameObject staffChoicePanelObj; // 스태프 선택 전체패널
    
    public GameObject selectPanel; // 스태프를 선택하는 패널

    [SerializeField] private GameObject staffPrefab         = null; // 스태프를 추가할 용도로 사용하는 게임오브젝트 프리팹
    [SerializeField] private GameObject staffListPanel      = null; //스태프를 자식으로 가지게될 패널
    [SerializeField] private Button[]   buttons             = null; //멤버를 선택할 수 있는 버튼들
    [SerializeField] private Button     pickUpExitButton    = null; //나가기 버튼
    [SerializeField] private Button     makeMusicButton     = null; //음악 만들기 버튼
    [SerializeField] private Button     choiceStaffButton   = null; //스태프 선택 버튼
    [SerializeField] private Button     noChoiceStaffButton = null; //스태프 선택 안함 버튼
    [SerializeField] private Button[]   sortButtons         = null;
    [SerializeField] private Text[]     statTexts           = null;
    [SerializeField] private Text       sortText            = null;

    //------------------스태프 목록 관련 변수-----------------
    [SerializeField] private Button     showStaffListButton      = null;
    [SerializeField] private Button     showStaffListLeftButton  = null;
    [SerializeField] private Button     showStaffListRightButton = null;
    [SerializeField] private GameObject showStaffListPanel       = null;
    [SerializeField] private Text       showStaffPageText        = null;
    [SerializeField] private int        showStaffCount = 0;

    //-----------------스태프 배치 관련 변수 ------------------
    [SerializeField] private Button staffSpawnButton          = null;  //스태프 배치 버튼    
    [SerializeField] private GameObject staffSpawnPanel       = null;  //스태프 배치 패널 오브젝트
    [SerializeField] private GameObject spawnStaffChoicePanel = null; // 스폰할 스태프 패널
    [SerializeField] private GameObject spawnStaffChoiceListPanel = null; // 스폰할 스태프 패널

    [SerializeField] private Button musicMakeStartButton = null;

    // -----------------이벤트 관련 변수 ------------------
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private GameObject oxPanel;
    private Vector3 eventStartPosition;

    [SerializeField] private GameObject dogamPanel = null;

    private string[] sayList = { "앞으로 잘 부탁드립니다!", "이 회사에 들어온게 꿈만 같아요!","앞으로 열심히 해보겠습니다!" };

    public int buttonCount = 0;
    private int staffCount = 0;

    [SerializeField] private int memberCount = 0;

    private List<GameObject> gachaStaff = new List<GameObject>();
    private GameObject explainStaffUnit = null;
    private GameObject showStaffUnit = null;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("이미 UI매니저가 있습니다.");
        }
        else
        {
            instance = this;
        }
        ButtonClick();
        //eventStartPosition = eventPanel.transform.position;
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

    public void StaffGatcha(StaffSO selectStaff,int index)
    {
        StaffManager.instance.staffList.RemoveAt(index);
        //Debug.Log("시작");
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

    public void CalendarSetting()
    {
        //calendarText.text = GameManager.instance.month + "월 " + GameManager.instance.day + "일";
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
            ShowExplane("돈이 부족합니다.");
        }
        staffPanel.SetActive(true);
        staffPanel.transform.DOScale(new Vector3(1.2f, 1.2f), 1.3f).OnComplete(() =>
        {
            ClearTween(staffGachaPanel);
            staffPanel.GetComponent<MyData>().isSelect = false;
            //GameObject staff = Instantiate(staffPanel.GetComponent<MyData>().myStaff.MySprite, staffSpawnPosition.position, Quaternion.identity);
            //staff.transform.parent = staffSpawnPosition.transform;
            //staff.transform.localScale = new Vector3(1f, 1f, 1f);
            NPC.NPC_Management.Instance.AddNPC(staffPanel.GetComponent<MyData>().myStaff);
            int randomIndex = UnityEngine.Random.Range(0, sayList.Length);
            //HumanEvent(staffPanel.GetComponent<MyData>().myStaff, sayList[randomIndex]);
            Dogam.instance.DogamChange(staffPanel.GetComponent<MyData>().myStaff.StaffNumber);
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
        StaffManager.instance.isSelectStaff = 0; // 원래 -1이였음.
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

    public void StaffGachaStart()
    {
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



    //---------스태프 선택 관련 함수-----------

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
        showStaffListButton.onClick.AddListener(() => { showStaffList(); });
        showStaffListLeftButton.onClick.AddListener(() => { showStaffLeft(); });
        showStaffListRightButton.onClick.AddListener(() => { showStaffRight(); });
        genreButton.onClick.AddListener(() => { genreChoicePanel.SetActive(true);
                                                GenreManager.instance.GenreSet(genreTransform); });
        staffSpawnButton.onClick.AddListener(() => { staffSpawnPanel.SetActive(true); });
        musicMakeStartButton.onClick.AddListener(() =>
        {
            GameManager.instance.curBPM = BPM_Management.Instance.curBPM;

            LoadingSceneManager.LoadScene("PianoScene"); 
        });
    }

    private readonly string[] STAFF_INFO_STRS = 
    {
        "이름 : ",
        "레벨 : ",
        "독창성 : ",
        "멜로디컬 : ",
        "중독성 : ",
        "대중성 : ",
        "좋아하는 장르 : ",
        "싫어하는 장르 : ",
        "직업 : ",
        "계약금 : "
    };

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

        for(int i = 0; i < _childCount; i++)
        {
            sb.Clear();
            sb.Append(STAFF_INFO_STRS[i]);
            sb.Append(_staffSO.GetInfos()[i]);
            if(i == 9) sb.Append("G");

            Text text = infoTrm.GetChild(i + 1).GetComponent<Text>();
            if(text != null) text.text = sb.ToString(); 
        }

        return staffUnit;
    }

    public void showDogam()
    {
        dogamPanel.SetActive(true);
        Camera.main.GetComponent<CameraSetting>().enabled = false;
        dogamPanel.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => { dogamPanel.SetActive(false);
            Camera.main.GetComponent<CameraSetting>().enabled = true;
        });
    }
    
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
            ShowExplane("직원이 없습니다.");
        }
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
            ShowExplane("3명 이상의 스태프를 선택해주세요.");   
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

    public void GachaGradeStart()
    {
        gachaGradePanel.SetActive(true);

        gradeButtons[0].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[0]); minusMoney = 500; });
        gradeButtons[1].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[1]); minusMoney = 1000; });
        gradeButtons[2].onClick.AddListener(() => { RealChoiceQuestion(gradeButtons[2]); minusMoney = 5000; }); 
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
            ShowExplane("돈이 부족합니다.");
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
                StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
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

    public void DistinctSelectWorkStaff(GameObject staffObj, GameObject staffUnit) //이미 있는 스태프를 수정하였을때
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
        moneyText.text = GameManager.instance.playerMoney.ToString() + "G";  
        starText.text = GameManager.instance.officeStar.ToString() + "성";
        staffCountText.text = StaffManager.instance.workStaffList.Count + "명";
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

/*            if (buttons[buttonCount - 1].GetComponent<Image>().sprite == null)
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { SelectWorkStaff(staff, staffUnit); });
            }
            else
            {
                staff.GetComponent<Button>().onClick.AddListener(() => { DistinctSelectWorkStaff(staff, staffUnit); });
            }*/
        }
    }


    public void notHumanEvent(string text, bool isOX)
    {
        if (isOX)
        {
            oxPanel.SetActive(true);
            eventPanel.SetActive(true);
            //StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
    }





}
