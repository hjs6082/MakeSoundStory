using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "Scriptable Object/EventSO", order = 1)]
public class EventSO : ScriptableObject
{
    [Header("이벤트 내용")][SerializeField]
    private string eventContents;
    public string EventContents { get { return eventContents; } }

    [Header("이벤트 사진")][SerializeField]
    private Sprite eventSprite;
    public Sprite EventSprite { get { return eventSprite; } }

    public enum eventType
    {
        VeryBad,          //스탯 - 10%감소
        Bad,              //스탯 - 5% 감소
        None,             //그냥 이벤트
        Good,             //스탯 5% 증가
        VeryGood,         //스탯 10% 증가
    }    

    public enum eventStat
    {
        None,                //스탯을 올려주는 이벤트가 아님.
        Money,               //돈 증가 이벤트
        Creativity,          //독창성 이벤트
        Addictive,           //중독성 이벤트
        Melodic,             //멜로디컬 이벤트
        Popularity,          //대중성 이벤트
        All                  //모든 스탯 이벤트
    }

    [SerializeField][Header("이벤트 타입")]
    private eventType myeventType; //이벤트 타입
    public eventType MyeventType { get { return myeventType; } }

    [SerializeField][Header("증가량(0도 가능) 및 감소량")]
    private int increment;
    public int Increment { get { return increment; } } 
}
