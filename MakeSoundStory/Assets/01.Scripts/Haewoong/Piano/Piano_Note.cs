using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Note : MonoBehaviour
    {
        private const float NOTE_SPEED = 3f;

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
            noteTrm.localPosition += Vector3.up * Time.deltaTime * curNoteSpeed;

            if(noteTrm.localPosition.y > 5.0f)
            DestroyNote();
        }

        public void DestroyNote()
        {
            Destroy(noteTrm.gameObject);
        }
    }
}