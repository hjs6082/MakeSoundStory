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

        public Action employ_Act = null;

        [SerializeField] private NPC_EmployManager employManager = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            /// NPC ÃÊ±âÈ­
            employ_Act += () => StartEmploy();
        }

        private void StartEmploy()
        {
            UIManager.instance.GachaGradeStart();
        }
    }
}
