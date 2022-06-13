using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney = 0;        //플레이어의 소지금
   
    public int allCreativity; // 현재 독창성
    public int allAddictive; // 현재 중독성
    public int allMelodic; // 현재 멜로디컬
    public int allPopularity; // 현재 대중성

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("이미 게임매니저가 존재합니다.");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
