using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSO", menuName = "Scriptable Object/BackgroundSO", order = 1)]
public class BackgroundSO : ScriptableObject
{
    [SerializeField][Header("��׶��� �ε���")]
    private int myIndex;
    public int MyIndex { get { return myIndex; } } 

    [SerializeField][Header("��׶��� �̸�")]
    private string placeName;
    public string PlaceName { get { return placeName; } }

    [SerializeField][Header("��׶��� ��������Ʈ")]
    private Sprite mySprite;
    public Sprite MySprite { get { return mySprite; } }

    [SerializeField][Header("��׶��忡 �����ϴ� ��������")]
    private List<StaffSO> myStaffs = new List<StaffSO>();
    public List<StaffSO> MyStaffs { get { return myStaffs; } }
}
