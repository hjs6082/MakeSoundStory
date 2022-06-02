using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField]
    private string staffName; // 이름
    public string StaffName { get { return staffName; } }

    [SerializeField]
    private string staffJob; // 직업
    public string StaffJob { get { return staffJob;  } }

    [SerializeField]
    private string favoriteGenre; //선호 장르
    public string FavoriteGenre { get { return favoriteGenre; } }

    [SerializeField]
    private string hateGenre; //선호 장르
    public string HateGenre { get { return hateGenre; } }

    [SerializeField]
    private int creativity; //독창성
    public int Creativity { get { return creativity; } }

    [SerializeField]
    private int addictive; //중독성
    public int Addictive { get { return addictive; } }

    [SerializeField]
    private int melodic; //멜로디컬함
    public int Melodic { get { return melodic; } }

    [SerializeField] 
    private int popularity; //멜로디컬함
    public int Popularity { get { return popularity; } }
}
