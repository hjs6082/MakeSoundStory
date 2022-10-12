using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Staff : MonoBehaviour 
{
    public Quest myQuest;
    public StaffSO myStaffData;

    private void Start()
    {
        StartCoroutine(PickupQuest());
    }
    
    IEnumerator PickupQuest()
    {
        yield return new WaitForSeconds(2f);
        if (EventManager.instance.isShowQuest == false)
        {
            Debug.Log("퀘스트뽑기");
            if (myStaffData.IsQuest == false)
            {
                if (Random.Range(0, 100) >= 50)
                {
                    myQuest = EventManager.instance.RandomQuest();
                    EventManager.instance.isShowQuest = true;
                    EventManager.instance.MoveQuest(myStaffData, "퀘스트가 도착했습니다.", myQuest,myQuest.item);
                }
            }
            else
            {
                foreach (var item in ItemManager.instance.itemList.items)
                {
                    if (item.itemName == myQuest.item.itemName)
                    {
                        if (item.count >= myQuest.count)
                        {
                            EventManager.instance.isShowQuest = true;
                            EventManager.instance.MoveQuest(myStaffData, "퀘스트를 완료했습니다.", myQuest,myQuest.item);
                        }
                    }
                }
                Debug.Log("이미 퀘스트가있습니다.");
            }
        }
            yield return new WaitForSeconds(60f);
            StartCoroutine(PickupQuest());

    }
}
