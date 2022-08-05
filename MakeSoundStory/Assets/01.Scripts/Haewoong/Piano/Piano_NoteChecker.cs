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
        }
    }
}