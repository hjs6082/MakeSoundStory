using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int number;           //넘버
    public string itemName;      //아이템 이름
    public int count;            //소지량
    public Sprite itemSprite; 
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public Item[] items; 
}
