using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public ItemSO itemList; 

    public Item RandomItem() //���� �������� �̴´�.
    {
        int randomIndex = Random.Range(0, itemList.items.Length);
        return itemList.items[randomIndex];
    }

    public int RandomCount() //�ʿ��� ������ ������ �̴´�.
    {
        return Random.Range(0, 3);
    }

    public ItemSO GetItemList()
    {
        return itemList;
    }
}
