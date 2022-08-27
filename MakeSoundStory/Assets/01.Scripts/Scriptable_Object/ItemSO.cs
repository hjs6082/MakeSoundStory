using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int number;           //넘버
    public int price;            //가격 
    public Sprite itemSprite;    //아이템 스프라이트
    public string itemName;      //아이템 이름
    public string itemExplane;   //아이템 설명
    public int maxamount;        //최대 수량
    public int amount;           //가지고있는 수량 
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public Item[] items; 
}
