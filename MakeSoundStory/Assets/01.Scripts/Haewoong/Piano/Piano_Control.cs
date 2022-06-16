using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Control : MonoBehaviour
    {
        public KeyCode[] pianoTile_Keys = new KeyCode[8] {
            KeyCode.S,
            KeyCode.D,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L
        };

        public void InitValue()
        {
            
        }
    }
}