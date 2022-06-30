using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSO", menuName = "Scriptable Object/BackgroundSO", order = 1)]
public class BackgroundSO : ScriptableObject
{
    [SerializeField][Header("백그라운드 인덱스")]
    private int myIndex;
    public int MyIndex { get { return myIndex; } } 

    [SerializeField][Header("백그라운드 이름")]
    private string placeName;
    public string PlaceName { get { return placeName; } }

    [SerializeField][Header("백그라운드 스프라이트")]
    private Sprite mySprite;
    public Sprite MySprite { get { return mySprite; } }

    [SerializeField][Header("백그라운드에 존재하는 스태프들")]
    private List<StaffSO> myStaffs = new List<StaffSO>();
    public List<StaffSO> MyStaffs { get { return myStaffs; } }
}
