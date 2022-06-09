using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool isOneClick = false; // �ѹ��� �ǰ� �ϴ� bool �Լ��� ��)������ ������ ���������� ������ �ȵǱ⶧���� 1ȸ������ �δ� ����.

    public int allCreativity; // ���� ��â��
    public int allAddictive; // ���� �ߵ���
    public int allMelodic; // ���� ��ε���
    public int allPopularity; // ���� ���߼�

    [SerializeField]
    private GameObject companyPanel; //ȸ�� �г� 

    //---------������ �̱� ���� ����-----------

    [SerializeField]
    private GameObject staffPanelObj;   //������ �г��� �ڽ����� �������ִ� �θ� ������Ʈ

    [SerializeField]
    private GameObject[] staffPanels; //�� 3���� �������� ��� �гε�;

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
    private Button choiceStaffButton; //������ ���� ��ư

    [SerializeField]
    private Button noChoiceStaffButton; //������ ���� ���� ��ư

    [SerializeField]
    private Text[] statTexts;

    public int buttonCount = 0;

    private int staffCount = 0;

    void Start()
    {
        if (instance != null)
        {
            Debug.Log("�̹� UI�Ŵ����� �ֽ��ϴ�.");
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
        //Debug.Log("����");
        int staffPanelsCount = staffPanelObj.transform.childCount;
        
        staffPanels = new GameObject[staffPanelsCount];
        for(int i = 0; i < staffPanels.Length; i++)
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



    //---------������ ���� ���� �Լ�-----------

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
        noChoiceStaffButton = noneStaff.GetComponent<Button>();
        noChoiceStaffButton.onClick.AddListener(() => { StaffNoChoice(); });

        for (int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
            {
            GameObject staff = Instantiate(staffPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            staff.transform.parent = staffListPanel.transform;
            staff.transform.localScale = new Vector3(1, 1, 1);

            StaffManager.instance.workStaffList.Sort(delegate (StaffSO a, StaffSO b)
            {
                if (a.StaffNumber > b.StaffNumber) return 1;
                else if (a.StaffNumber < b.StaffNumber) return -1;
                return 0;
            });

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
        allCreativity = 0;
        allAddictive = 0;
        allMelodic = 0;
        allPopularity = 0;
    }

    public void ShowStat()
    {
        statTexts[0].text = allCreativity.ToString();
        statTexts[1].text = allAddictive.ToString();
        statTexts[2].text = allMelodic.ToString();
        statTexts[3].text = allPopularity.ToString();
    }

    public void StatSetting()
    {
        StatReset();
        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            allCreativity += StaffManager.instance.pickWorkStaffList[i].Creativity;
            allAddictive += StaffManager.instance.pickWorkStaffList[i].Addictive;
            allMelodic += StaffManager.instance.pickWorkStaffList[i].Melodic;
            allPopularity += StaffManager.instance.pickWorkStaffList[i].Popularity;
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

    }

    public void StaffNoChoice()
    {
        for(int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if(buttons[buttonCount - 1].GetComponent<Image>().sprite == StaffManager.instance.pickWorkStaffList[i].MySprite)
            {
                allAddictive -= StaffManager.instance.pickWorkStaffList[i].Addictive;
                allCreativity -= StaffManager.instance.pickWorkStaffList[i].Creativity;
                allMelodic -= StaffManager.instance.pickWorkStaffList[i].Melodic;
                allPopularity -= StaffManager.instance.pickWorkStaffList[i].Popularity;
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
}
