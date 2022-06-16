using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    struct NoteInfo
    {
        public GameObject _note;
        public Vector2 _notePos;
        public bool _isNull;
    }

    public class Piano_NoteSpawner : MonoBehaviour
    {
        private const float SPAWN_DELAY = 0.75f;

        [SerializeField]
        private GameObject[] note_Prefabs = null;
        public Image[] guideLines = null;
        public List<int> lineIdxs = null;

        public float spawnDelay = 0.0f;
        private bool bPlaying = false;

        private void Awake()
        {
            
        }

        private void Start()
        {
            StartPiano();
        }

        public void InitValue()
        {
            spawnDelay = SPAWN_DELAY;
            guideLines = Piano_Management.Instance.guideLine_Parent.GetComponentsInChildren<Image>();
            lineIdxs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        }

        private void StartPiano()
        {
            bPlaying = true;
            StartCoroutine(SpawnNote());
        }

        private void InsNote(int _lineIdx)
        {
            NoteInfo noteInfo;

            if     (_lineIdx == 0 || _lineIdx == 1) { noteInfo = SetNote(0, _lineIdx); }
            else if(_lineIdx == 2 || _lineIdx == 3) { noteInfo = SetNote(1, _lineIdx); }
            else if(_lineIdx == 4 || _lineIdx == 5) { noteInfo = SetNote(2, _lineIdx); }
            else                                    { noteInfo = SetNote(3, _lineIdx); }

            GameObject note = Instantiate(noteInfo._note, noteInfo._notePos, Quaternion.identity, this.transform);
            Piano_Management.Instance.spawned_Note_List.Add(note);
        }

        private NoteInfo SetNote(int _noteIdx, int _lineIdx)
        {
            NoteInfo noteInfo = new NoteInfo();
            
            noteInfo._note = note_Prefabs[_noteIdx];

            noteInfo._notePos.x = guideLines[_lineIdx].transform.position.x;
            noteInfo._notePos.y = Piano_Management.Instance.guideLine_Parent.position.y;

            return noteInfo;
        }

        private IEnumerator SpawnNote()
        {
            while(bPlaying)
            {
                int spawnCount = UnityEngine.Random.Range(1, 5);
                for(int i = 0; i < spawnCount; i++)
                {
                    int randomLine = UnityEngine.Random.Range(0, guideLines.Length - i);

                    for(int j = randomLine; j < lineIdxs.Count - 1; i++)
                    {
                        lineIdxs[j] = lineIdxs[j + 1];
                    }   
                    lineIdxs[lineIdxs.Count - 1] = randomLine;

                    InsNote(randomLine);
                }

                lineIdxs.Sort();
                bPlaying = false;
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
