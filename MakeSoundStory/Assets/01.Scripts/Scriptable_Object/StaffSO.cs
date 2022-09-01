using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField][Header("스태프 레벨")]
    private int stafflevel; // 스태프 레벨
    public int StaffLevel { get { return stafflevel; } }
    
    [SerializeField][Header("스태프 번호")]
    private int staffNumber; // 스태프 넘버
    public int StaffNumber { get { return staffNumber; } }

    [SerializeField][Header("이름")]
    private string staffName; // 이름
    public string StaffName { get { return staffName; } }

    [SerializeField][Header("직업")]
    private string staffJob; // 직업
    public string StaffJob { get { return staffJob;  } }

    public enum Genre
    {
        팝,
        알앤비,
        재즈,
        트랩,
        붐뱁,
        발라드,
        트로트,
        락,
        펑크,
        댄스, 
        없음
    }

    [SerializeField][Header("선호 장르")]
    private Genre favoriteGenre; //선호 장르

    public Genre FavoriteGenre { get { return favoriteGenre; } }

    [SerializeField] [Header("불호 장르")]
    private Genre hateGenre; //선호 장르

    public Genre HateGenre { get { return hateGenre; } }

    public enum tried
    {
        normal,
        bad, 
        good
    }

    [SerializeField] [Header("피로도")]
    private tried staffTried; //선호 장르

    public tried StaffTried { get { return staffTried; } }

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

    [SerializeField][Header("직원 사진")]
    private GameObject mySprite;
    public GameObject MySprite { get { return mySprite; } } 

    public object[] GetInfos()
    {
        object[] infos = new object[11]
        {
            staffName,
            stafflevel,
            creativity,
            melodic,
            addictive,
            popularity,
            hateGenre,
            favoriteGenre,
            staffJob,
            money,
            staffNumber
        };
        
        return infos;
    }
}
