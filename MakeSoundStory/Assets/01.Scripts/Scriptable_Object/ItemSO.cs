using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int number;           //�ѹ�
    public int price;            //���� 
    public Sprite itemSprite;    //������ ��������Ʈ
    public string itemName;      //������ �̸�
    public string itemExplane;   //������ ����
    public int maxamount;        //�ִ� ����
    public int amount;           //�������ִ� ���� 
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public Item[] items; 
}
