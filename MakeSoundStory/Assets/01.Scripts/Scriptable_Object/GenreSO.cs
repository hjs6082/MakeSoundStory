using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenreSO", menuName = "Scriptable Object/GenreSO", order = 1)]
public class GenreSO : ScriptableObject
{
    [SerializeField][Header("장르넘버")]
    private int genreIdx; //선호 장르
    public int GenreIndex { get { return genreIdx; } }

    [SerializeField][Header("장르넘버")]
    private string genreName; //선호 장르
    public string GenreName { get { return genreName; } }

    [SerializeField][Header("장르넘버")]
    private string genreExplane; //선호 장르
    public string GenreExplane { get { return genreExplane; } }
}
