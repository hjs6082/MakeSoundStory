using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StaffManager : MonoBehaviour
{
    public static StaffManager instance;

    [Header("��ü ������ ����Ʈ")]
    public List<StaffSO> staffList = new List<StaffSO>();

    [Header("���� ������ ����Ʈ")]
    public List<StaffSO> pickStaffList = new List<StaffSO>();

    [Header("���� ���ϰ��ִ� ������ ����Ʈ")]
    public List<StaffSO> workStaffList = new List<StaffSO>();

    [Header("���� ���ۿ� ���� ������ ����Ʈ")]
    public List<StaffSO> pickWorkStaffList = new List<StaffSO>();

    [Header("�ް��� �� ������ ����Ʈ")]
    public List<StaffSO> triedStaffList = new List<StaffSO>(); 

    public int isSelectStaff;

    private string[] sayList = { "�ȳ��ϼ���, ������ ���׿�.", "�˾� ����, �����ϰ� ���� ����. �ٵ� ���̶��� �����ֳ�..\n�׳� ��ٷ����ڰ�" };

    [SerializeField]
    private GameObject npcTransform;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("�̹� �������Ŵ����� �ֽ��ϴ�.");
        }
        else
        {
            instance = this;
        }
        AddStaff();
    }

    void Update()
    {
        
    }

    public void AddStaff()
    {
        staffList = new List<StaffSO>(Resources.LoadAll<StaffSO>("StaffSO"));
        staffList.Sort(delegate (StaffSO a, StaffSO b)
        {
            if (a.StaffNumber > b.StaffNumber) return 1;
            else if (a.StaffNumber < b.StaffNumber) return -1;
            return 0;
        });
    }

    public void RandomStaff()
    {
        if (staffList != null)
        {
            int randoxIndex = Random.Range(0, staffList.Count);
            StaffSO selectStaff = staffList[randoxIndex];
            pickStaffList.Add(staffList[randoxIndex]);
            //staffList.RemoveAt(randoxIndex); 

            UIManager.instance.StaffGatcha(selectStaff,randoxIndex);

            if (workStaffList.Count != 0)
            {
                Debug.Log("243");
                staffList.Clear();
                StaffSO[] staffs = (Resources.LoadAll<StaffSO>("StaffSO"));
                for (int i = 0; i < staffs.Length; i++)
                {
                    staffList.Add(staffs[i]);
                }

                for (int i = 0; i < workStaffList.Count; i++)
                {
                    staffList.Remove(workStaffList[i]);
                }
            }
            if(pickStaffList.Count == 6)
            {
                pickStaffList.Clear();
                staffList.Clear();
                StaffSO[] staffs = (Resources.LoadAll<StaffSO>("StaffSO"));
                for (int i = 0; i < staffs.Length; i++)
                {
                    staffList.Add(staffs[i]);
                }

                for (int i = 0; i < workStaffList.Count; i++)
                {
                    staffList.Remove(workStaffList[i]);
                }
            }  

        }
    }

    public StaffSO ReturnRandomStaff()
    {
        int randoxIndex = Random.Range(0, staffList.Count);
        return workStaffList[randoxIndex];
    }

    public void Say(StaffSO staff)
    {
        //��ȭâ�� �����ؾ���. ��ȭâ �ȿ� ������ �̹����� �ְ�, ���ϴ°��� �������� �����ְ� ���̾�α׸� ����. 
        int randomIndex = Random.Range(0, sayList.Length);
        
    }

    public void NoneTalk(StaffSO staff)
    {
        UIManager.instance.staffTalkText.DOKill();
        if(UIManager.instance.staffImage.transform.childCount != 0)
        {
            Destroy(UIManager.instance.staffImage.transform.GetChild(0).gameObject);
        }
        int randomIndex = Random.Range(0, sayList.Length);
        UIManager.instance.staffTalkPanel.SetActive(true);
        UIManager.instance.staffTalkText.text = "";
        UIManager.instance.staffTalkNameText.text = "";
        UIManager.instance.staffTalkText.DOText(sayList[randomIndex], 3f);
        UIManager.instance.staffTalkNameText.text = staff.StaffName;
        GameObject staffImage = Instantiate(staff.StaffPrefab, UIManager.instance.staffImage.position, Quaternion.identity); 
        staffImage.transform.GetChild(0).gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 2;
        staffImage.transform.localScale = new Vector2(2f, 2f);
        staffImage.transform.parent = UIManager.instance.staffImage;
    }

    public void Talk(StaffSO staff, string talkText)
    {
        Debug.Log("Talk");
        UIManager.instance.staffTalkText.DOKill();
        if (UIManager.instance.staffImage.transform.childCount != 0)
        { 
            Destroy(UIManager.instance.staffImage.transform.GetChild(0).gameObject);
        }
            UIManager.instance.staffTalkPanel.SetActive(true);
            UIManager.instance.staffTalkText.text = "";
            UIManager.instance.staffTalkNameText.text = "";
            UIManager.instance.staffTalkText.DOText(talkText, 1f);
            UIManager.instance.staffTalkNameText.text = staff.StaffName;
            GameObject staffImage = Instantiate(staff.StaffPrefab, UIManager.instance.staffImage.position, Quaternion.identity);
            staffImage.transform.GetChild(0).gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 2;
            staffImage.transform.localScale = new Vector2(2f, 2f);
            staffImage.transform.SetParent(UIManager.instance.staffImage);
       
    }


    public void StopNpc()
    {
        for (int i = 0; i < npcTransform.transform.childCount; i++)
        {
            npcTransform.transform.GetChild(i).gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void RunNpc()
    {
        for (int i = 0; i < npcTransform.transform.childCount; i++)
        {
            npcTransform.transform.GetChild(i).gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    IEnumerator StaffEvent()
    {
        if(workStaffList.Count != 0)
        {
            int randomIndex = Random.Range(0, workStaffList.Count);
            StaffSO randomStaff = workStaffList[randomIndex];
            //UIManager.instance.HumanEvent(randomIndex,);
        }
        else
        {
            yield return new WaitForSeconds(60f);
            StartCoroutine(StaffEvent());
        }
    }
}
