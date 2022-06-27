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

    [SerializeField]
    private GameObject eventPanel; // 이벤트 창을 띄워줄 이벤트 패널

    [SerializeField]
    private bool isEvent = false;


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
        StartCoroutine(CoroutineEvent());
    }


    void Update()
    {
        ClosePanel();
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
}
