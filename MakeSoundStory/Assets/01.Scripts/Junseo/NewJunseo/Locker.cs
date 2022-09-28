using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locker : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject[] itemObjs;
    [SerializeField] private Transform itemParent; 

    private void Start()
    {
        ItemSetting(ItemManager.instance.GetItemList());   
    } 

    // Update is called once per frame
    void Update()
    {
         
    }

    public void ItemSetting(ItemSO itemList) // 아이템 UI를 설정해준다.
    {
        itemObjs = new GameObject[itemList.items.Length];
        for(int i = 0; i < itemList.items.Length; i++)
        {
            GameObject item = Instantiate(itemPrefab, itemParent);
            item.name = "item" + i;
            item.transform.GetChild(0).GetComponent<Text>().text = itemList.items[i].itemName;
            item.transform.GetChild(1).GetComponent<Text>().text = itemList.items[i].count.ToString();
            itemObjs[i] = item;
        } 
    }

    public void ItemReSetting(ItemSO itemList)
    {
        for(int i = 0; i < itemObjs.Length; i++)
        {
            itemObjs[i].transform.GetChild(0).GetComponent<Text>().text = itemList.items[i].itemName;
            itemObjs[i].transform.GetChild(1).GetComponent<Text>().text = itemList.items[i].count.ToString();
        }
    }
}
