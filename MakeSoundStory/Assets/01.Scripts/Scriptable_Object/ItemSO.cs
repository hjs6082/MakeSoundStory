using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int number;           //�ѹ�
    public string itemName;      //������ �̸�
    public int count;            //������
    public Sprite itemSprite; 
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public Item[] items; 
}
