using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Update()
        {
        
        }

        public bool CheckNote(int _hitIdx)
        {
            Piano_Note note = sameLine_Notes[0];
            if(note.transform.position.y - this.transform.position.y <= 0.4f)
            {
                Piano_Management.Instance.P_Stat.IncreaseScore(_hitIdx % 4);

                sameLine_Notes.Remove(note);
                note.DestroyNote();

                return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Piano_Note note = other.GetComponent<Piano_Note>();

            if(note != null)
            {
                Debug.Log("노트 충돌");
                sameLine_Notes.Add(note);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Piano_Note note = other.GetComponent<Piano_Note>();

            if(note != null)
            {
                sameLine_Notes.Remove(note);
                Destroy(other.gameObject);
            }
        }
    }
}