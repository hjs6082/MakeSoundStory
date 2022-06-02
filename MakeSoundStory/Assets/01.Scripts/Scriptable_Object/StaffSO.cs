using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField]
    private string staffName; // 이름
    public string StaffName { get { return staffName; } }

    [SerializeField][Header("직업")]
    private string staffJob; // 직업
    public string StaffJob { get { return staffJob;  } }

    [SerializeField][Header("선호 장르")]
    private string favoriteGenre; //선호 장르
    public string FavoriteGenre { get { return favoriteGenre; } } 

    [SerializeField] [Header("불호 장르")]
    private string hateGenre; //싫어하는 장르
    public string HateGenre { get { return hateGenre; } }

    [SerializeField][Header("독창성")]
    private int creativity; //독창성
    public int Creativity { get { return creativity; } }

    [SerializeField][Header("중독성")]
    private int addictive; //중독성
    public int Addictive { get { return addictive; } }

    [SerializeField][Header("멜로디컬")]
    private int melodic; //멜로디컬함
    public int Melodic { get { return melodic; } }

    [SerializeField] [Header("대중성")]
    private int popularity; //대중성   
    public int Popularity { get { return popularity; } }

    [SerializeField][Header("계약금")]
    private int money; //계약금
    public int Money { get { return money; } }
}
