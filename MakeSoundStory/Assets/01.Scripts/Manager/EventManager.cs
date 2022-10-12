using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("이벤트 목록")]
    public List<EventSO> eventList = new List<EventSO>();

    [Header("퀘스트 목록")]
    public List<Quest> questList = new List<Quest>();

    public List<GameObject> questObjList = new List<GameObject>();

    [SerializeField]
    private GameObject eventPanel; // 이벤트 창을 띄워줄 이벤트 패널

    [SerializeField]
    private GameObject moveEventPrefab; // 이벤트 창을 띄워줄 이벤트 패널

    [SerializeField]
    private Transform moveEventTrm;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questTrm;

    public QuestSO questSO; 

    [SerializeField]
    private bool isEvent = false;

    public bool isShowQuest = false;

    [SerializeField] private ChoiceSO choiceList;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Transform choiceTrm;
    [SerializeField] Button[] choiceButtons;


    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("이미 이벤트 매니저가 있습니다.");
        }
        AddEvent();
        QuestSetting();
        StartCoroutine(CoroutineEvent());
    }


    void Update()
    {
        ClosePanel();
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("243");
            MoveChoice(StaffManager.instance.ReturnRandomStaff()); 
        }
    }

    public void ClosePanel()
    {
        if(eventPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                eventPanel.SetActive(false);
            }
        }
    }

    public void AddEvent()
    {
        EventSO[] events = (Resources.LoadAll<EventSO>("EventSO"));
        for (int i = 0; i < events.Length; i++)
        {
            eventList.Add(events[i]);
        }
    }

    IEnumerator CoroutineEvent()
    {
        while (!isEvent)
        {
            yield return new WaitForSeconds(35f);
            Debug.Log(3);
            MakeEvent();
        }
    }

    public void MakeEvent()
    {
        if (Random.Range(1, 100) >= 80)
        {
            isEvent = true;
            StopCoroutine(CoroutineEvent());
            eventPanel.SetActive(true);
            eventPanel.transform.DOScale(new Vector3(1.0f, 1.0f), 0.5f).OnComplete(() =>
            {
                SelectEvent(eventPanel);
            });
        }
        else
        {

        }
    }

    public void SelectEvent(GameObject setPanel)
    {
        int randoxIndex = Random.Range(0, eventList.Count);
        EventSO selectEvent = eventList[randoxIndex]; 
        setPanel.transform.GetChild(2).GetComponent<Image>().sprite = selectEvent.EventSprite;
        setPanel.transform.GetChild(3).GetComponent<Text>().text = selectEvent.EventContents;
    }

    public void CloseEventPanel()
    {
        eventPanel.SetActive(false);
        eventPanel.transform.localScale = new Vector3(0f, 0f);
        isEvent = false;
        StartCoroutine(CoroutineEvent());
    }

    public void MoveQuest(StaffSO staff, string text, Quest pickEvent,Item item) 
    {
        GameObject moveEventPanel = Instantiate(moveEventPrefab,moveEventTrm);
        if (moveEventPanel.transform.GetChild(0).gameObject.transform.childCount != 0)
        {
            Destroy(moveEventPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
        }
        moveEventPanel.SetActive(true);
        moveEventPanel.GetComponent<Button>().onClick.AddListener(() => 
        {
            if (staff.IsQuest == false)
            {
                ItemManager.instance.GetQuest(staff, pickEvent);
                moveEventPanel.SetActive(false);
            }
            else
            {
                GameManager.instance.playerDebt += pickEvent.reward;
                RemoveQuest(pickEvent.number);
                questList.Remove(pickEvent);
                item.count -= pickEvent.count;
                staff.IsQuest = false; 
            }
            moveEventPanel.transform.DOLocalMove(new Vector2(1408f, -310f), 0.5f).OnComplete(() => { Destroy(moveEventPanel); isShowQuest = false; }); 
        });
        GameObject setStaff = Instantiate(staff.StaffHeadPrefab, moveEventPanel.transform.GetChild(0).gameObject.transform);
        setStaff.transform.localPosition = new Vector3(0f,0f,500f); 
        moveEventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
        moveEventPanel.transform.DOLocalMoveX(519f, 1f);
    }

    public void MoveChoice(StaffSO staff)
    {
        GameObject moveEventPanel = Instantiate(moveEventPrefab, moveEventTrm);
        if (moveEventPanel.transform.GetChild(0).gameObject.transform.childCount != 0)
        {
            Destroy(moveEventPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
        }
        moveEventPanel.SetActive(true);
        moveEventPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChoiceStart(staff, RandomChoice());
            moveEventPanel.transform.DOLocalMove(new Vector2(1408f, -310f), 0.5f).OnComplete(() => { Destroy(moveEventPanel); });
        });

        GameObject setStaff = Instantiate(staff.StaffHeadPrefab, moveEventPanel.transform.GetChild(0).gameObject.transform);
        setStaff.transform.localPosition = new Vector3(0f, 0f, 500f);
        moveEventPanel.transform.GetChild(1).GetComponent<Text>().text = "상담을 원하는데요..";
        moveEventPanel.transform.DOLocalMoveX(519f, 1f);
    }

    public Choice RandomChoice()
    {
        int RandomIndex = Random.Range(0, choiceList.choices.Length);
        return choiceList.choices[RandomIndex];
    }

    public void ChoiceStart(StaffSO staff, Choice choice)
    {
        int selectChoice;
        choicePanel.SetActive(true);
        if(choiceTrm.transform.GetChild(0).childCount != 0)
        {
            Destroy(choiceTrm.transform.GetChild(0).GetChild(0).gameObject);
        }
        Instantiate(staff.StaffHeadPrefab, choiceTrm.transform.GetChild(0).gameObject.transform);
        choiceTrm.transform.GetChild(1).gameObject.GetComponent<Text>().text = staff.StaffName;
        choiceTrm.transform.GetChild(2).gameObject.GetComponent<Text>().text = choice.choiceQuestion;
        choiceTrm.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = choice.choiceOne;
        choiceTrm.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = choice.choiceTwo;
        choiceTrm.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Text>().text = choice.choiceThree;
        for(int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[index].onClick.AddListener(() => { selectChoice = i + 1; AnswerCheck(selectChoice, choice); choicePanel.SetActive(false); });
        }
    }

    public void AnswerCheck(int selectIndex, Choice choice)
    {
        if(choice.answer == selectIndex)
        {
            ItemManager.instance.RandomItem().count += 2;
        }
    }

    public void AddQuest(Quest quest, StaffSO staff)
    {
        Transform[] allChildren = questTrm.GetComponentsInChildren<Transform>();

        GameObject questObj = Instantiate(questPrefab, questTrm);
        Instantiate(staff.StaffHeadPrefab, questObj.transform.GetChild(0).gameObject.transform);
        questObj.transform.GetChild(1).GetComponent<Text>().text = quest.item.itemName + "" + quest.count + "개를 모아오세요.";
        questObj.transform.GetChild(2).GetComponent<Text>().text = "보상 : 음표 " + quest.reward + "개";
        questObjList.Add(questObj);
        questObj.name = "Quest" + quest.number;
        Transform trm = DistanceQuest(allChildren, questObjList);
        if (trm != null)
        {
            Destroy(trm.gameObject);
        }
    }

    public Transform DistanceQuest(Transform[] parent, List<GameObject> objList)
    {
        foreach (Transform item in parent)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                if (item.name == objList[i].name)
                {
                    return item.transform;
                }
            }
        }
        return null;
    }
    
    public void RemoveQuest(int index)
    {
        Transform[] allChildren = questTrm.GetComponentsInChildren<Transform>();

        foreach (Transform item in allChildren)
        {
            if (item.name == "Quest" + index)
            {
                Destroy(item.gameObject);
            }
        }
    }

    
    public void QuestSetting() 
    {
        foreach (var item in questSO.quests)
        {
            for (int i = 0; i < item.count; i++)
            {
                item.number = i;
            }
            item.item = ItemManager.instance.RandomItem();
            item.count = Random.Range(1, 4);
            item.reward = Random.Range(1, 4) * 5;
        }
    }

    public Quest RandomQuest()
    {
        int randomIndex = Random.Range(0, questSO.quests.Length);
        return questSO.quests[randomIndex];
    }
}
