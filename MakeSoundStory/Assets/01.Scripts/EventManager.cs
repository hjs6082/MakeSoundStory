using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("�̺�Ʈ ���")]
    public List<EventSO> eventList = new List<EventSO>();

    [SerializeField]
    private GameObject eventPanel; // �̺�Ʈ â�� ����� �̺�Ʈ �г�

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
            Debug.Log("�̹� �̺�Ʈ �Ŵ����� �ֽ��ϴ�.");
        }
        AddEvent();
        StartCoroutine(CoroutineEvent());
    }


    void Update()
    {
        
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
            yield return new WaitForSeconds(30f);
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

            });
        }
        else
        {

        }
    }

    public void CloseEventPanel()
    {
        eventPanel.SetActive(false);
        eventPanel.transform.localScale = new Vector3(0f, 0f);
        isEvent = false;
        StartCoroutine(CoroutineEvent());
    }
}
