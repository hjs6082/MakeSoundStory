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
            Debug.Log("����Ʈ�̱�");
            if (myStaffData.IsQuest == false)
            {
                if (Random.Range(0, 100) >= 50)
                {
                    myQuest = EventManager.instance.RandomQuest();
                    EventManager.instance.isShowQuest = true;
                    EventManager.instance.MoveQuest(myStaffData, "����Ʈ�� �����߽��ϴ�.", myQuest,myQuest.item);
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
                            EventManager.instance.MoveQuest(myStaffData, "����Ʈ�� �Ϸ��߽��ϴ�.", myQuest,myQuest.item);
                        }
                    }
                }
                Debug.Log("�̹� ����Ʈ���ֽ��ϴ�.");
            }
        }
            yield return new WaitForSeconds(60f);
            StartCoroutine(PickupQuest());

    }
}
