using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LunchPad
{
    public class Pad_Spawner : MonoBehaviour
    {
        private const float MAX_GRID_SIZE = 845.0f;
        
        [Range(3, 6)]
        public int pad_Grid_Count = 4;
        
        public GameObject pad_Btn_Prefab = null;
        public Transform  lunchPad_Parent = null;

        public List<Pad_Button> pad_Btn_List = null;

        public WaitForSeconds wait = null;

        public void InitValue()
        {
            pad_Btn_List = new List<Pad_Button>();
            wait = new WaitForSeconds(Pad_Management.Instance.bpmSpeed);

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
                Text padText = pad.GetComponentInChildren<Text>();

                padBtn.ownIdx = idx;
                padText.text = idx.ToString();

                pad_Btn_List.Add(padBtn);           
            }
        }

        public IEnumerator PlayLunch()
        {
            for(int i = 0; i < pad_Btn_List.Count; i++)
            {
                pad_Btn_List[i].Play();

                yield return new WaitForSeconds(pad_Btn_List[i].ownClip.length);
            }
        }
    }
}