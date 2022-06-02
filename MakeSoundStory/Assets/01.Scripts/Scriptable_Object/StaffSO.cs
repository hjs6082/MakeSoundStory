using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Object/StaffSO", order = 1)]
public class StaffSO : ScriptableObject
{
    [SerializeField]
    private string staffName; // �̸�
    public string StaffName { get { return staffName; } }

    [SerializeField]
    private string staffJob; // ����
    public string StaffJob { get { return staffJob;  } }

    [SerializeField]
    private string favoriteGenre; //��ȣ �帣
    public string FavoriteGenre { get { return favoriteGenre; } }

    [SerializeField]
    private string hateGenre; //��ȣ �帣
    public string HateGenre { get { return hateGenre; } }

    [SerializeField]
    private int creativity; //��â��
    public int Creativity { get { return creativity; } }

    [SerializeField]
    private int addictive; //�ߵ���
    public int Addictive { get { return addictive; } }

    [SerializeField]
    private int melodic; //��ε�����
    public int Melodic { get { return melodic; } }

    [SerializeField] 
    private int popularity; //��ε�����
    public int Popularity { get { return popularity; } }
}
