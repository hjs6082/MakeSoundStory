using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenreSO : ScriptableObject
{
    [SerializeField][Header("�帣�ѹ�")]
    private int genreIndex; //��ȣ �帣
    public int GenreIndex { get { return genreIndex; } }

    [SerializeField][Header("�帣�ѹ�")]
    private string genreName; //��ȣ �帣
    public string GenreName { get { return genreName; } }

    [SerializeField][Header("�帣�ѹ�")]
    private int genreIndex; //��ȣ �帣
    public int GenreIndex { get { return genreIndex; } }
}
