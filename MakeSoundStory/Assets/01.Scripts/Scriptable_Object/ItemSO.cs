using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    [SerializeField][Header("아이템 넘버")]
    private int itemNumber;
    public int ItemNumber { get { return itemNumber; } }

    [SerializeField][Header("아이템 이름")]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField][Header("아이템 효과")]
    private string itemEffect;
    public string ItemEffect { get { return itemEffect; } }

    public enum itemStat
    {
        creativity, //독창성
        addictive,  //중독성
        melodic,    //멜로디컬
        popularity  //대중성
    }
    public itemStat ItemStat; 

    public enum itemPercentage
    {
        small, //작음 5% 올려줌
        midium, //중간 10% 올려줌
        large // 큼 15% 올려줌
    }

    public itemPercentage ItemPercentage; 
}
