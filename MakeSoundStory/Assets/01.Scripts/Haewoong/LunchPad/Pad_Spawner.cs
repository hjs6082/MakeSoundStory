using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LunchPad
{
    public class Pad_Spawner : MonoBehaviour
    {
        private const float MAX_GRID_SIZE = 845.0f;
        
        public int pad_Grid_Count = 4;
        
        public GameObject pad_Btn_Prefab = null;
        public Transform  lunchPad_Parent = null;

        public List<Pad_Button> pad_Btn_List = null;

        public void InitValue()
        {
            pad_Btn_List = new List<Pad_Button>();
            
            InitLunchPad();
        }

        public void InitLunchPad()
        {
            GridLayoutGroup gridGroup = lunchPad_Parent.GetComponent<GridLayoutGroup>();

            int gridCount = (int)Mathf.Pow(pad_Grid_Count, 2);
            float gridSpacing = (pad_Grid_Count - 1) * 15;
            float gridSizeOffset = (MAX_GRID_SIZE - gridSpacing) / pad_Grid_Count;

            gridGroup.constraintCount = pad_Grid_Count;
            gridGroup.cellSize = new Vector2(gridSizeOffset, gridSizeOffset);

            for(int i = 0; i < gridCount; i++)
            {
                int idx = i;

                GameObject pad = Instantiate(pad_Btn_Prefab, lunchPad_Parent);
                Pad_Button padBtn = pad.GetComponent<Pad_Button>();

                padBtn.ownIdx = idx;

                pad_Btn_List.Add(padBtn);           
            }
        }
    }
}