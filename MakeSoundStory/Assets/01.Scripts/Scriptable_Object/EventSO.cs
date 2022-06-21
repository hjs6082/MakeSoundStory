using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "Scriptable Object/EventSO", order = 1)]
public class EventSO : ScriptableObject
{
    [Header("�̺�Ʈ ����")][SerializeField]
    private string eventContents;
    public string EventContents { get { return eventContents; } }

    [Header("�̺�Ʈ ����")][SerializeField]
    private Sprite eventSprite;
    public Sprite EventSprite { get { return eventSprite; } }

    public enum eventType
    {
        VeryBad,          //���� - 10%����
        Bad,              //���� - 5% ����
        None,             //�׳� �̺�Ʈ
        Good,             //���� 5% ����
        VeryGood,         //���� 10% ����
    }    

    public enum eventStat
    {
        None,                //������ �÷��ִ� �̺�Ʈ�� �ƴ�.
        Money,               //�� ���� �̺�Ʈ
        Creativity,          //��â�� �̺�Ʈ
        Addictive,           //�ߵ��� �̺�Ʈ
        Melodic,             //��ε��� �̺�Ʈ
        Popularity,          //���߼� �̺�Ʈ
        All                  //��� ���� �̺�Ʈ
    }

    [SerializeField][Header("�̺�Ʈ Ÿ��")]
    private eventType myeventType; //�̺�Ʈ Ÿ��
    public eventType MyeventType { get { return myeventType; } }

    [SerializeField][Header("������(0�� ����) �� ���ҷ�")]
    private int increment;
    public int Increment { get { return increment; } } 
}
