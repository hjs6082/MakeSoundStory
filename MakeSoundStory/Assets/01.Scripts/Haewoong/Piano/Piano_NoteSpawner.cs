using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public struct NoteInfo
    {
        public GameObject _note;
        public Vector2 _notePos;
        public bool _isNull;
    }

    public class Piano_NoteSpawner : MonoBehaviour
    {
        //private const float SPAWN_DELAY = 0.75f;

        [SerializeField] private GameObject[] note_Prefabs = null;
        [SerializeField] private Transform    note_Parent  = null;

        public Image[] guideLines = null;
        public List<int> lineIdxs = null;
        
        public void InitValue()
        {
            guideLines = Piano_Management.Instance.guideLine_Parent.GetComponentsInChildren<Image>();
            lineIdxs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

            Piano_Management.Instance.noteCheck_Act += (x) => InsNote(x);
        }

        private GameObject InsNote(int _lineIdx)
        {
            NoteInfo noteInfo;

            if     (_lineIdx == 0 || _lineIdx == 1) { noteInfo = SetNote(0, _lineIdx); }
            else if(_lineIdx == 2 || _lineIdx == 3) { noteInfo = SetNote(1, _lineIdx); }
            else if(_lineIdx == 4 || _lineIdx == 5) { noteInfo = SetNote(2, _lineIdx); }
            else                                    { noteInfo = SetNote(3, _lineIdx); }

            GameObject note = Instantiate(noteInfo._note, noteInfo._notePos, guideLines[_lineIdx].rectTransform.rotation, this.transform);
            note.transform.localScale = new Vector2(0.75f, 0.75f);

            return note;
        }

        private NoteInfo SetNote(int _noteIdx, int _lineIdx)
        {
            NoteInfo noteInfo = new NoteInfo();
            
            noteInfo._note = note_Prefabs[_noteIdx];
            noteInfo._notePos = guideLines[_lineIdx].transform.position;

            return noteInfo;
        }
    }
}
