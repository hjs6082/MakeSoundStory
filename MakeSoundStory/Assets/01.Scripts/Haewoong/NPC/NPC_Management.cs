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

        public NPC_FSM[] npc_FSMs = null;

        private void Awake()
        {
            InitValue();
        }

        private void InitValue()
        {
            InitSington();

            npc_FSMs = GetComponentsInChildren<NPC_FSM>();
        }

        public void AddNPC(StaffSO npc_Prefab)
        {
            for(int i = 0; i < npc_FSMs.Length; i++)
            {
                if(npc_FSMs[i].npc_Unit_Prefab == null)
                {
                    npc_FSMs[i].npc_Unit_Prefab = npc_Prefab.StaffPrefab;
                    npc_FSMs[i].Init();
                    npc_FSMs[i].npc_Unit.AddComponent<StaffData>();
                    npc_FSMs[i].npc_Unit.GetComponent<StaffData>().myStaffData = npc_Prefab;
                    npc_FSMs[i].npc_Unit.AddComponent<Staff>();
                    npc_FSMs[i].npc_Unit.GetComponent<Staff>().myStatus = UIManager.instance.staffStatus;
                    npc_FSMs[i].npc_Unit.GetComponent<Staff>().noneTalkSprite = UIManager.instance.talkImage;
                    return;
                }
            }
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
    }
}
