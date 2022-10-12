using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int number;
    public Item item;
    public int count;
    public int reward;
}

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Object/QuestSO", order =1)]
public class QuestSO : ScriptableObject
{
    public Quest[] quests;
}
