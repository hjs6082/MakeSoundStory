using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField]
    private string staffName; // �̸�
    public string StaffName { get { return staffName; } }

    [SerializeField][Header("����")]
    private string staffJob; // ����
    public string StaffJob { get { return staffJob;  } }

    [SerializeField][Header("��ȣ �帣")]
    private string favoriteGenre; //��ȣ �帣
    public string FavoriteGenre { get { return favoriteGenre; } } 

    [SerializeField] [Header("��ȣ �帣")]
    private string hateGenre; //�Ⱦ��ϴ� �帣
    public string HateGenre { get { return hateGenre; } }

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
}
