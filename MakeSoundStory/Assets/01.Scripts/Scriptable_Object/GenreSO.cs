using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenreSO : ScriptableObject
{
    [SerializeField][Header("장르넘버")]
    private int genreIndex; //선호 장르
    public int GenreIndex { get { return genreIndex; } }

    [SerializeField][Header("장르넘버")]
    private string genreName; //선호 장르
    public string GenreName { get { return genreName; } }

    [SerializeField][Header("장르넘버")]
    private int genreIndex; //선호 장르
    public int GenreIndex { get { return genreIndex; } }
}
