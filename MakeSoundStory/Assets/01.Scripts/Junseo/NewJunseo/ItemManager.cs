using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;

public class ItemManager : MonoSingleton<ItemManager>
{
    public ItemSO itemList;

    public GameObject questPanel;
    public Text       questContents;
    public Transform  questStaffTrm; 
    public Text       rewardText;
    public Button[]   questButtons;


    public Item RandomItem() //랜덤 아이템을 뽑는다.
    {
        int randomIndex = Random.Range(0, itemList.items.Length);
        return itemList.items[randomIndex];
    }

    public int RandomCount() //필요한 아이템 개수를 뽑는다.
    {
        return Random.Range(0, 3);
    }

    public ItemSO GetItemList()
    {
        return itemList;
    }

    public void GetQuest(StaffSO staff, Quest quest)
    {
        if(questStaffTrm.transform.childCount != 0)
        {
            Destroy(questStaffTrm.transform.GetChild(0).gameObject);
        }
        questPanel.SetActive(true);
        GameObject staffObj = Instantiate(staff.StaffHeadPrefab, questStaffTrm);
        questContents.text = quest.item.itemName + " " + quest.count + "개를 모아다주세요."; 
        rewardText.text = "보상 : " + quest.reward + " 음표";
        questButtons[0].onClick.AddListener(() => 
        {   
            EventManager.instance.questList.Add(quest);
            EventManager.instance.AddQuest(quest,staff);
            questPanel.SetActive(false); 
            staff.IsQuest = true;
            EventManager.instance.isShowQuest = false;
        });
        questButtons[1].onClick.AddListener(() => { questPanel.SetActive(false); EventManager.instance.isShowQuest = false; }); 
    }
}
