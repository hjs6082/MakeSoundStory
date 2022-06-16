using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano_Management : MonoBehaviour
{
    public  static Piano_Management Instance => instance;
    private static Piano_Management instance = null;

    public static Action destroyNote_Act = null;

    /// <summary>
    /// 0 : 독창성, 1 : 대중성, 2 : 멜로디컬, 3 : 중독성
    /// </summary>
    public int[] total_Stats = new int[4];
    public List<GameObject> spawned_Note_List = null;
    public Image[] guideLines = null;

    private void Awake()
    {
        InitValue();
    }

    private void InitSingleton()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("다수의 매니지먼트");
        Destroy(this.gameObject);
    }

    private void InitValue()
    {
        InitSingleton();

        if(GameManager.instance != null)
        {
            total_Stats[0] = GameManager.instance.allCreativity; // 독창성
            total_Stats[1] = GameManager.instance.allPopularity; // 대중성
            total_Stats[2] = GameManager.instance.allMelodic;    // 멜로디컬
            total_Stats[3] = GameManager.instance.allAddictive;  // 중독성  
        }

        spawned_Note_List = new List<GameObject>(); 
    }
}
