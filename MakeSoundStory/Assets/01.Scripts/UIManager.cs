using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //---------스태프 뽑기 관련 변수-----------

    [SerializeField]
    private GameObject staffPanelObj;   

    [SerializeField]
    private GameObject[] staffPanels;

    [SerializeField]
    private GameObject clearPanel;

    public GameObject staffGachaPanel;

    //---------스태프 선택 관련 변수-----------

    public GameObject selectPanel;

    [SerializeField]
    private GameObject staffPrefab;

    [SerializeField]
    private GameObject members;




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
    }

    void Update()
    {

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
        staffPanels[staffCount].GetComponent<MyData>().myStaff = selectStaff;

            if (staffCount != 3)
            {
                staffCount++;
                if (staffCount == 3)
                {
                    staffCount = 0;
                }
            }

        //DistinctCheck(); 
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
            clearPanel.SetActive(true);
            clearPanel.transform.DOScale(new Vector3(2.5f, 2.2f), 0.5f).OnComplete(() => 
            {
                staffGachaPanel.SetActive(false);
                clearPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f);
            });
        });
    }

    public void StaffGachaStart()
    {
        for (int i = 0; i < 3; i++)
        {
            StaffManager.instance.RandomStaff();
        }
    }



    //---------스태프 선택 관련 함수-----------

    public void SelectPanelClick()
    {
        selectPanel.SetActive(true);

        for(int i = 0; i < StaffManager.instance.workStaffList.Count; i++)
        {
            GameObject staff = Instantiate(staffPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            staff.transform.parent = members.transform;
            staff.transform.localScale = new Vector3(1, 1, 1);  
            staff.GetComponent<Image>().sprite = StaffManager.instance.workStaffList[i].MySprite;   
        }

    } 
}
