using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyInfoSO", menuName = "Scriptable Object/KeyInfoSO", order = 0)]
public class KeyInfoSO : ScriptableObject
{
    public KeyCode keyCode;
    public Sprite keyImage;
}
