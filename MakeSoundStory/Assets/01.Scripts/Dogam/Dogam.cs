using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dogam : MonoBehaviour
{
    [SerializeField]
    private GameObject dogamObj;
    [SerializeField]
    private GameObject dogamPrefab;
    [SerializeField]
    private Text dogamCountText;
    [SerializeField]
    private GameObject[] dogamPanels;
    public static Dogam instance;
    public Dictionary<int, bool> dogamDictionary = new Dictionary<int, bool>();

    private int dogamCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DogamStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DogamChange(int Number)
    {
        if (dogamDictionary.ContainsKey(Number))
        {
            dogamDictionary.Remove(Number);
            dogamDictionary.Add(Number, true);
            foreach (var item in dogamDictionary)
            {
                if(item.Value == true)
                {
                    dogamPanels[item.Key - 1].GetComponent<Image>().color = Color.blue;
                }
            }
        }
        dogamCount = 0;
        foreach (var item in dogamDictionary)
        {
            if (item.Value == true)
            {
                dogamCount++;
            }
        }
        dogamCountText.text = dogamDictionary.Count + "명 중 " + dogamCount + " 명의 스태프 발견";
    }

    public void DogamStart()
    {
        for (int i = 0; i < StaffManager.instance.staffList.Count; i++)
        {
            GameObject dogam = Instantiate(dogamPrefab, new Vector3(0f,0f), Quaternion.identity);
            dogam.transform.parent = dogamObj.transform;
            dogam.transform.localScale = new Vector3(1f, 1f);
            dogam.GetComponent<StaffData>().myStaffData = StaffManager.instance.staffList[i];
        }
        int dogamListCount = dogamObj.transform.childCount;
        dogamPanels = new GameObject[dogamListCount];
        for (int i = 0; i < dogamListCount; i++)
        {
            dogamPanels[i] = dogamObj.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < dogamPanels.Length; i++)
        {
            dogamPanels[i].transform.GetChild(0).GetComponent<Text>().text = "#" + dogamPanels[i].GetComponent<StaffData>().myStaffData.StaffNumber.ToString();
            //dogamPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = dogamPanels[i].GetComponent<MyData>().myData.CharacterImg; <<아트가 없기때문에 아직 못넣었어요.
            dogamPanels[i].transform.GetChild(2).GetComponent<Text>().text = dogamPanels[i].GetComponent<StaffData>().myStaffData.StaffName;
            dogamPanels[i].transform.GetChild(3).GetComponent<Text>().text = dogamPanels[i].GetComponent<StaffData>().myStaffData.StaffJob;
            dogamDictionary.Add(dogamPanels[i].GetComponent<StaffData>().myStaffData.StaffNumber, false);
        }
        dogamCountText.text = dogamDictionary.Count + "명 중 " + 0 + " 명의 스태프 발견";
    }

}
