using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dot_Management : MonoBehaviour
{
    private static Dot_Management instance = null;
    public static Dot_Management Instance => instance;

    public Action makeStart_Act = null;
    public Action<KeyCode, int> addScore_Act = null;
    public Action<GameObject, Vector2> bezier_Act = null;

    private Dot_Spawner dot_Spawner = null;
    private Dot_Controller dot_Controller = null;
    private Dot_Checker dot_Checker = null;
    private Dot_Graph dot_Graph = null;

    [SerializeField] private Image[] guide_Lines = new Image[4];
    [SerializeField] private TextMeshProUGUI[] stat_Count_Texts = new TextMeshProUGUI[4];
    [SerializeField] private GameObject[] dot_Prefabs = new GameObject[4];

    public int dotPrefabsCount = 0;
    public Transform dot_Parent = null;
    public List<Dot_Obj> cur_List = null;

    public int[] stat_Counts = new int[4];

    private void Awake()
    {
        InitSingleton();
        InitValue();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && !dot_Spawner.isPlaying)
        {
            makeStart_Act?.Invoke();
        }   
    }

    private void InitSingleton()
    {
        if(instance != null)
        {
            Debug.LogError($"다수의 {this.transform.name} 실행 중");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void InitValue()
    {
        dot_Spawner = GetComponent<Dot_Spawner>();
        dot_Controller = GetComponent<Dot_Controller>();
        dot_Checker = FindObjectOfType<Dot_Checker>();
        dot_Graph = GetComponent<Dot_Graph>();

        dotPrefabsCount = dot_Prefabs.Length;
        for(int i = 0; i < stat_Counts.Length; i++)
        {
            stat_Counts[i] = 0;
        }
        
        makeStart_Act += () => dot_Spawner.MakeStart();
        addScore_Act += (x, y) => AddStatCount(x, y);
    }

    public bool CheckDots(KeyCode _keyCode, int _idx)
    {
        Dot_Obj obj = dot_Checker.hit_Dot.Find((x) => x.keyCode == _keyCode);

        if(obj != null) 
        {
            obj.isBezier = true;
            obj.dot = obj.gameObject;
            obj.SetObjects(dot_Graph.block_Parents[_idx].position);
            return true;
        }
        return false;
    }

    public void AddStatCount(KeyCode _keyCode, int _idx)
    {
        cur_List = dot_Checker.hit_Dot;

        if(CheckDots(_keyCode, _idx))
        {
            stat_Counts[_idx]++;
            UpdateText(_idx);
            dot_Graph.AddBlock(_keyCode, _idx);
        }
    }

    private void UpdateText(int _idx)
    {
        stat_Count_Texts[_idx].text = stat_Counts[_idx].ToString();
    }

    public Transform GetGuideLineTrm(int _idx)
    {
        return guide_Lines[_idx].transform;
    }

    public GameObject GetDot(int _idx)
    {
        return dot_Prefabs[_idx];
    }

    public TextMeshProUGUI GetStatText(int _idx)
    {
        return stat_Count_Texts[_idx];
    }

    public Dot_Spawner GetSpawner()
    {
        return dot_Spawner;
    }

    public Dot_Controller GetController()
    {
        return dot_Controller;
    }

    public Dot_Graph GetGraph()
    {
        return dot_Graph;
    }
}
