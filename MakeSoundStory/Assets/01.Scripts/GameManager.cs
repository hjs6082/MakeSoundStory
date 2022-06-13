using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney = 0;        //�÷��̾��� ������
   
    public int allCreativity; // ���� ��â��
    public int allAddictive; // ���� �ߵ���
    public int allMelodic; // ���� ��ε���
    public int allPopularity; // ���� ���߼�

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("�̹� ���ӸŴ����� �����մϴ�.");
        }
        UIManager.instance.GameStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
