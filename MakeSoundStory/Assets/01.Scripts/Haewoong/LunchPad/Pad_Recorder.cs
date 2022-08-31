using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunchPad
{
    public class Pad_Recorder : MonoBehaviour
    {
        public bool isRec = false;
        public List<int> record_Sound = null;

        public void InitValue()
        {
            record_Sound = new List<int>();
        }

        public void Record(int _idx)
        {
            if(isRec)
            {
                record_Sound.Add(_idx);
            }
        }
    }
}