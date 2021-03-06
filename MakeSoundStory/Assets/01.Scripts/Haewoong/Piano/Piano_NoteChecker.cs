using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class Piano_NoteChecker : MonoBehaviour
    {
        [SerializeField]
        private Transform noteHits_Parent = null;
        public Piano_NoteHit[] noteHits = null;

        public void InitValue()
        {
            noteHits = noteHits_Parent.GetComponentsInChildren<Piano_NoteHit>();
            Piano_Management.Instance.noteCheck_Act += (x) => CheckNote(x);
        }

        private void CheckNote(int _hitIdx)
        {
            Piano_NoteHit noteHit = noteHits[_hitIdx];
            int noteCount = noteHit.sameLine_Notes.Count;

            if(noteCount > 0)
            {
                noteHit.CheckNote(_hitIdx);
            }
        }
    }
}