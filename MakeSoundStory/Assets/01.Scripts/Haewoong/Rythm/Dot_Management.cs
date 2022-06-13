using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public List<Dot_NormalObj> cur_List = null;

    public uint[] stat_Increament = new uint[4];
    public uint[] stat_Counts = new uint[4];

    [Header("제작 후")]
    public GameObject buildName_Panel = null;
    public InputField soundName_Input = null;
    public GameObject protoEnd_Panel = null; // 삭제 예정
    public TextMeshProUGUI soundName = null;
    public TextMeshProUGUI stats_TMP = null;

    private void Awake()
    {
        InitSingleton();
        InitValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !dot_Spawner.isPlaying)
        {
            makeStart_Act?.Invoke();
        }
    }

    private void InitSingleton()
    {
        if (instance != null)
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

        if (GameManager.instance != null)
        {
            int staffCount = StaffManager.instance.pickWorkStaffList.Count;

            stat_Increament[0] = 1 + (uint)(GameManager.instance.allPopularity / staffCount);
            stat_Increament[1] = 1 + (uint)(GameManager.instance.allMelodic    / staffCount);
            stat_Increament[2] = 1 + (uint)(GameManager.instance.allAddictive  / staffCount);
            stat_Increament[3] = 1 + (uint)(GameManager.instance.allCreativity / staffCount);
        }
        else
        {
            for(int i = 0; i < stat_Increament.Length; i++)
            {
                stat_Increament[i] = 1;
            }
        }

        for (int i = 0; i < stat_Counts.Length; i++)
        {
            stat_Counts[i] = 0;
        }

        makeStart_Act += () => dot_Spawner.MakeStart();
        addScore_Act += (x, y) => AddStatCount(x, y);
    }

    public bool CheckDots(KeyCode _keyCode, int _idx)
    {
        Dot_NormalObj obj = dot_Checker.hit_Dot.Find((x) => x.keyCode == _keyCode);

        if (obj != null)
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

        if (CheckDots(_keyCode, _idx))
        {
            stat_Counts[_idx] += stat_Increament[_idx];
            UpdateText(_idx);
            dot_Graph.AddBlock(_keyCode, _idx);
        }
    }

    private void UpdateText(int _idx)
    {
        stat_Count_Texts[_idx].text = stat_Counts[_idx].ToString();
    }

    public void EndMakeSound()
    {
        // 곡 이름 짓기 패널 ON
        buildName_Panel.SetActive(true);
    }

    public void UploadSound()
    {
        // ProtoEndPanel ON
        soundName.text = string.Format("곡 이름 : {0}", soundName_Input.text);
        stats_TMP.text = string.Format("대중성 : {0}\n멜로디컬 : {1}\n중독성 : {2}\n독창성 : {3}", stat_Counts[0], stat_Counts[1], stat_Counts[2], stat_Counts[3]);

        protoEnd_Panel.SetActive(true);
    }

    public void Continue()
    {
        // 씬 넘기기

        SceneManager.LoadScene("SceneJunSeo");
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