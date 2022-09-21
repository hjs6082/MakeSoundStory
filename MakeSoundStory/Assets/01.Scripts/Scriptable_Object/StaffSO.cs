using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField][Header("������ ����")]
    private int stafflevel; // ������ ����
    public int StaffLevel { get { return stafflevel; } }
    
    [SerializeField][Header("������ ��ȣ")]
    private int staffNumber; // ������ �ѹ�
    public int StaffNumber { get { return staffNumber; } }

    [SerializeField][Header("�̸�")]
    private string staffName; // �̸�
    public string StaffName { get { return staffName; } }

    [SerializeField][Header("����")]
    private string staffJob; // ����
    public string StaffJob { get { return staffJob;  } }

    public enum Genre
    {
        팝,
        RnB,
        재즈,
        트랩,
        붐뱁,
        발라드,
        트로트,
        락,
        펑크,
        디스코
    }

    [SerializeField][Header("��ȣ �帣")]
    private Genre favoriteGenre; //��ȣ �帣
    public Genre FavoriteGenre { get { return favoriteGenre; } }

    [SerializeField] [Header("��ȣ �帣")]
    private Genre hateGenre; //��ȣ �帣
    public Genre HateGenre { get { return hateGenre; } }

    public enum tried
    {
        normal,
        bad, 
        good
    }

    [SerializeField] [Header("�Ƿε�")]
    private tried staffTried; //��ȣ �帣
    public tried StaffTried { get { return staffTried; } }

    [SerializeField] [Header("��Ʈ����")]
    private int stress;
    public int Stress { get { return stress; } }

    [SerializeField][Header("��â��")]
    private int creativity; //��â��
    public int Creativity { get { return creativity; } }

    [SerializeField][Header("�ߵ���")]
    private int addictive; //�ߵ���
    public int Addictive { get { return addictive; } }

    [SerializeField][Header("��ε���")]
    private int melodic; //��ε�����
    public int Melodic { get { return melodic; } }

    [SerializeField] [Header("���߼�")]
    private int popularity; //���߼�   
    public int Popularity { get { return popularity; } }

    [SerializeField][Header("����")]
    private int money; //����
    public int Money { get { return money; } }

    [SerializeField][Header("���� ����")]
    private GameObject staffPrefab;
    public GameObject StaffPrefab { get { return staffPrefab; } } 
}
