using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Spawner : MonoBehaviour
{
    private const float DEFAULT_SPAWN_DELAY = 1f;
    private const float MAX_PLAYING_TIME = 90.0f;

    private List<Dot_Obj> dotList = null;

    private float spawn_Delay = 0.0f;
    private float playingTime = 0.0f;

    public bool isPlaying = false;

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

    }

    private void InitValue()
    {
        spawn_Delay = DEFAULT_SPAWN_DELAY;
        playingTime = 0.0f;

        dotList = new List<Dot_Obj>();
    }

    public void MakeStart()
    {
        isPlaying = true;
        StartCoroutine(RepeatSpawn());
    }

    private Dot_Obj SpawnDot(int _idx)
    {        
        GameObject dot_Prefab = Dot_Management.Instance.GetDot(_idx);
        Transform  guide_Line = Dot_Management.Instance.GetGuideLineTrm(_idx);
        Vector3    dot_SpawnPoint = new Vector3(10.0f, guide_Line.position.y, 0.0f);

        GameObject dot = Instantiate(dot_Prefab, dot_SpawnPoint, Quaternion.identity, Dot_Management.Instance.dot_Parent);

        Dot_Obj dot_Obj = dot.GetComponent<Dot_Obj>();

        return dot_Obj;
    }

    private IEnumerator RepeatSpawn()
    {
        while(isPlaying)
        {
            dotList.Clear();

            for(int i = 0; i < Dot_Management.Instance.dotPrefabsCount; i++)
            {
                int randSp = Random.Range(0, 10);

                if(randSp < 5)
                {
                    dotList.Add(SpawnDot(i));
                }
            }

            yield return new WaitForSeconds(spawn_Delay);
        }
    }
}
