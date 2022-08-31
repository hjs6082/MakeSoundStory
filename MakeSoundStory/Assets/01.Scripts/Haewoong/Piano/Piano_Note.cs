using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Note : MonoBehaviour
    {
        private Transform noteTrm = null;
        public bool isTap = false;

        private void Awake()
        {
            InitValue();
        }

        public void InitValue()
        {
            noteTrm = GetComponent<Transform>();

            isTap = false;
        }

        public void Tap()
        {
            isTap = true;
        }

        public void DestroyNote()
        {
            Destroy(noteTrm.gameObject);
        }
    }
}