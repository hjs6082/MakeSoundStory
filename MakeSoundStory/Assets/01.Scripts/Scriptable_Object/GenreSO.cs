using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenreSO", menuName = "Scriptable Object/GenreSO", order = 1)]
public class GenreSO : ScriptableObject
{
    [SerializeField][Header("�帣�ѹ�")]
    private int genreIdx; //��ȣ �帣
    public int GenreIndex { get { return genreIdx; } }

    [SerializeField][Header("�帣�ѹ�")]
    private string genreName; //��ȣ �帣
    public string GenreName { get { return genreName; } }

    [SerializeField][Header("�帣�ѹ�")]
    private string genreExplane; //��ȣ �帣
    public string GenreExplane { get { return genreExplane; } }
}
