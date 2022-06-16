using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_NoteSpawner : MonoBehaviour
    {
        private const float SPAWN_DELAY = 0.75f;

        [SerializeField]
        private GameObject[] note_Prefabs = null;
        private float[] rand_Percentage = null;

        public float spawnDelay = 0.0f;
        private bool bPlaying = false;

        private void Awake()
        {
            InitValue();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S)) StartPiano();
        }

        private void InitValue()
        {
            spawnDelay = SPAWN_DELAY;
            rand_Percentage = new float[Piano_Management.Instance.total_Stats.Length];
            //InitRandPercentage();
        }

        private void InitRandPercentage()
        {
            int[] stats = Piano_Management.Instance.total_Stats;
            float totalAllStat = 0;

            for(int i = 0; i < stats.Length; i++)
            {
                totalAllStat += stats[i];
            }

            for(int i = 0; i < stats.Length; i++)
            {
                rand_Percentage[i] = (stats[i] / totalAllStat) * 100.0f;

                if(i > 0)
                {
                    rand_Percentage[i] += rand_Percentage[i - 1];
                }

                Debug.Log(rand_Percentage[i]);
            }
        }

        private void StartPiano()
        {
            bPlaying = true;
            StartCoroutine(SpawnNote());
        }

        private IEnumerator SpawnNote()
        {
            while(bPlaying)
            {
                GameObject notePrefab = null;
                Vector2 notePos = new Vector2(0, 0);
                float randPer = UnityEngine.Random.Range(0.0f, 100.0f);

                // if(randPer < rand_Percentage[0])      { notePrefab = note_Prefabs[0]; }
                // else if(randPer < rand_Percentage[1]) { notePrefab = note_Prefabs[1]; }
                // else if(randPer < rand_Percentage[2]) { notePrefab = note_Prefabs[2]; }
                // else                                  { notePrefab = note_Prefabs[3]; }

                if(randPer < 10.0f)      { notePrefab = note_Prefabs[0]; notePos.y = Piano_Management.Instance.guideLines[0].transform.position.y; }
                else if(randPer < 30.0f) { notePrefab = note_Prefabs[1]; notePos.y = Piano_Management.Instance.guideLines[0].transform.position.y; }
                else if(randPer < 70.0f) { notePrefab = note_Prefabs[2]; notePos.y = Piano_Management.Instance.guideLines[0].transform.position.y; }
                else                     { notePrefab = note_Prefabs[3]; notePos.y = Piano_Management.Instance.guideLines[0].transform.position.y; }

                GameObject note = Instantiate(notePrefab, notePos, Quaternion.identity, this.transform);
                Piano_Management.Instance.spawned_Note_List.Add(note);

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
