using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Piano
{
    public class Piano_NoteHit : MonoBehaviour
    {
        public List<Piano_Note> sameLine_Notes = null;

        private void Awake()
        {
            InitValue();
        }

        public void InitValue()
        {
            sameLine_Notes = new List<Piano_Note>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Piano_Note note = other.GetComponent<Piano_Note>();

            if(note != null)
            {
                Debug.Log("��Ʈ �浹");
                sameLine_Notes.Add(note);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Piano_Note note = other.GetComponent<Piano_Note>();

            if(note != null)
            {
                sameLine_Notes.Remove(note);

                if(note.bMiss)
                {
                    note.MissNote();
                } 

                note.DestroyNote();
            }
        }
    }
}