using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object
{
    public int price;
    public string objName;
    public string objExplane;
}

[CreateAssetMenu(fileName = "ObjectSO", menuName = "Scriptable Object/ObjectSO")]
public class ObjectSO : ScriptableObject
{
    public ObjectSO[] objects;
}
