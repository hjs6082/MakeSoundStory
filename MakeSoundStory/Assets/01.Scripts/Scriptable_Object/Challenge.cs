using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChallengeInfo
{
    public int challengeNumber;
    public string challengeName;
    public int needValue;
    public int getGold;
    public enum type
    {
        staff,
        gold,
        music
    }
    public type challengeType;
    public bool isClear;
}


[CreateAssetMenu(fileName = "Challenges", menuName ="Scriptable Object/Challenges", order =1)]
public class Challenge : ScriptableObject
{
    public ChallengeInfo[] challenges;
}
