using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    [SerializeField][Header("������ �ѹ�")]
    private int itemNumber;
    public int ItemNumber { get { return itemNumber; } }

    [SerializeField][Header("������ �̸�")]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField][Header("������ ȿ��")]
    private string itemEffect;
    public string ItemEffect { get { return itemEffect; } }

    public enum itemStat
    {
        creativity, //��â��
        addictive,  //�ߵ���
        melodic,    //��ε���
        popularity  //���߼�
    }
    public itemStat ItemStat; 

    public enum itemPercentage
    {
        small, //���� 5% �÷���
        midium, //�߰� 10% �÷���
        large // ŭ 15% �÷���
    }

    public itemPercentage ItemPercentage; 
}
