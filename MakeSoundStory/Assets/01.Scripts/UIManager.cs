using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //---------������ �̱� ���� ����-----------

    [SerializeField]
    private GameObject staffPanelObj;   

    [SerializeField]
    private GameObject[] staffPanels;

    [SerializeField]
    private GameObject clearPanel;

    public GameObject staffGachaPanel;

    //---------������ ���� ���� ����-----------

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
            Debug.Log("�̹� UI�Ŵ����� �ֽ��ϴ�.");
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
        //Debug.Log("����");
        int staffPanelsCount = staffPanelObj.transform.childCount;
        
        staffPanels = new GameObject[staffPanelsCount];
        for(int i = 0; i < staffPanels.Length; i++)
        {
            staffPanels[i] = staffPanelObj.transform.GetChild(i).gameObject;
        }

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



    //---------������ ���� ���� �Լ�-----------

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
