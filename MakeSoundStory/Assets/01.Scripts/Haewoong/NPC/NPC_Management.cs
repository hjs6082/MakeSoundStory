using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_Management : MonoBehaviour
    {
        private static NPC_Management instance = null;
        public static NPC_Management Instance => instance;

        public Action employ_Start_Act = null;
        public Action employ_End_Act = null;

        [SerializeField] private NPC_EmployManager employManager = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            InitSington();

            /// NPC √ ±‚»≠
            employManager = FindObjectOfType<NPC_EmployManager>();
            employ_Start_Act += () => StartEmploy();
            employ_End_Act += () => EndEmploy();
        }

        private void InitSington()
        {
            if(instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this.gameObject);
        }

        private void StartEmploy()
        {
            UIManager.instance.GachaGradeStart();
        }

        private void EndEmploy()
        {
            UIManager.instance.GachaGradeEnd();
        }
    }
}
