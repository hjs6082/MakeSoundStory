using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_EmployManager : NPC_Base
    {
        protected override void Awake()
        {
            InitValue();
        }

        protected override void Start()
        {
            
        }

        protected override void Update()
        {
            
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("hjkdhjksdfgjk");
            NPC_Management.Instance.employ_Start_Act?.Invoke();
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            NPC_Management.Instance.employ_End_Act?.Invoke();
        }

        protected override void InitValue()
        {
            
        }

        protected override void Ready()
        {
            
        }

        protected override void Talk()
        {
            
        }
    }
}
