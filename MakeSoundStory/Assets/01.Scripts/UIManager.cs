using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject staffPanelObj;

    [SerializeField]
    private GameObject[] staffPanels;

    public static UIManager instance;

    private int staffCount = 0;

    private bool isDistinct = false;

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
        //StaffGatcha();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StaffManager.instance.RandomStaff();
            StaffManager.instance.RandomStaff();
            StaffManager.instance.RandomStaff();
        }
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
}
