using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Spawner : MonoBehaviour
{
    private static readonly System.Random DOT_RANDOM = new System.Random();

    private const float DEFAULT_SPAWN_DELAY = 1f;
    private const float MAX_PLAYING_TIME = 60.0f;


    public List<Dot_NormalObj> dotList = null;

    private float spawn_Delay = 0.0f;
    private float playingTime = 0.0f;

    private int randomDot = 0;

    public bool isPlaying = false;
    public bool bOnce = false;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        if(isPlaying)
        {
            playingTime += Time.deltaTime;

            if(playingTime >= MAX_PLAYING_TIME)
            {
                playingTime = 0.0f;
                isPlaying = false;
            }
        }
        else if(dotList.Count == 0 && bOnce)
        {
            Dot_Management.Instance.EndMakeSound();
            bOnce = false;
        }
    }

    private void InitValue()
    {
        spawn_Delay = DEFAULT_SPAWN_DELAY;
        playingTime = 0.0f;

        dotList = new List<Dot_NormalObj>();
    }

    public void MakeStart()
    {
        isPlaying = true;
        if(!bOnce) bOnce = true;
        StartCoroutine(RepeatSpawn());
    }

    private Dot_NormalObj SpawnDot(int _idx)
    {        
        GameObject dot_Prefab = Dot_Management.Instance.GetDot(_idx);
        Transform  guide_Line = Dot_Management.Instance.GetGuideLineTrm(_idx);
        Vector3    dot_SpawnPoint = new Vector3(10.0f, guide_Line.position.y, 0.0f);

        GameObject dot = Instantiate(dot_Prefab, dot_SpawnPoint, Quaternion.identity, Dot_Management.Instance.dot_Parent);

        Dot_NormalObj dot_Obj = dot.GetComponent<Dot_NormalObj>();

        return dot_Obj;
    }

    private IEnumerator RepeatSpawn()
    {
        while(isPlaying)
        {
            dotList.Clear();

            randomDot = DOT_RANDOM.Next(0, 100);

            if (randomDot < 95)
            {
                for (int i = 0; i < Dot_Management.Instance.dotPrefabsCount; i++)
                {
                    int randSp = Random.Range(0, 10);

                    if (randSp < 5)
                    {
                        dotList.Add(SpawnDot(i));
                    }
                }
            }
            else
            {
                Debug.Log("특수 노트!");
            }

            yield return new WaitForSeconds(spawn_Delay);
        }
    }
}
