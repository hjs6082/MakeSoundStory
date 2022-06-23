using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Note : MonoBehaviour
    {
        private const float NOTE_SPEED = 2.5f;

        private Transform noteTrm = null;
        private float curNoteSpeed = 0.0f;
        public bool bMiss = true;

        private void Awake()
        {
            InitValue();
        }

        private void Update()
        {
            MoveNote();
        }

        public void InitValue()
        {
            noteTrm = GetComponent<Transform>();
            curNoteSpeed = NOTE_SPEED;
        }

        private void MoveNote()
        {
            noteTrm.position += Vector3.down * Time.deltaTime * curNoteSpeed;
        }

        public void DestroyNote()
        {
            Piano_Management.Instance.spawned_Note_List.Remove(noteTrm.gameObject);
            Destroy(noteTrm.gameObject);
        }

        public void MissNote()
        {
            Piano_Management.Instance.combo = -1;
            Piano_Management.Instance.UpdateCombo();
        }
    }
}