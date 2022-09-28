using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public ItemSO itemList; 

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
}
