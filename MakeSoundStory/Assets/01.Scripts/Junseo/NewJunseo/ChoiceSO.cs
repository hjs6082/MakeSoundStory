using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public int number;
    [TextArea]public string choiceQuestion;
    public string choiceOne;
    public string choiceTwo; 
    public string choiceThree;
    public int answer;
}

[CreateAssetMenu(fileName = "ChoiceSO", menuName = "Scriptable Object/ChoiceSO", order = 1)]
public class ChoiceSO : ScriptableObject
{
    public Choice[] choices;
}
