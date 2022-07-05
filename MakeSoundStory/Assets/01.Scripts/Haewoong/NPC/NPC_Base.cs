using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public abstract class NPC_Base : MonoBehaviour
    {
        protected abstract void Awake();
        protected abstract void Start();
        protected abstract void Update();

        protected abstract void OnTriggerEnter2D(Collider2D other);
        protected abstract void OnTriggerExit2D(Collider2D other);

        protected abstract void InitValue();
        protected abstract void Ready();
        protected abstract void Talk();
    }
}