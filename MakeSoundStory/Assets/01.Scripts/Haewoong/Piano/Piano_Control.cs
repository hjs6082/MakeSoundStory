using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class Piano_Control : MonoBehaviour
    {
        private Dictionary<KeyCode, int> pianoTile_Dic = null;
        public Sprite[] pianoTile_Key_Images = null;
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
            pianoTile_Dic = new Dictionary<KeyCode, int>();
            for (int i = 0; i < pianoTile_Keys.Length; i++)
            {
                pianoTile_Dic.Add(pianoTile_Keys[i], i);
            }

            Piano_Management.Instance.noteInput_Act += () => InputPiano();
        }

        private void InputPiano()
        {
            if (Input.anyKeyDown)
            {
                foreach (var tile in pianoTile_Dic)
                {
                    if (Input.GetKeyDown(tile.Key))
                    {
                        Piano_Management.Instance.noteCheck_Act?.Invoke(tile.Value);
                        ChangeGuideLineColor(tile.Value, tile.Value);
                    }
                }
            }

            foreach (var tile in pianoTile_Dic)
            {
                if (Input.GetKeyUp(tile.Key))
                {
                    ChangeGuideLineColor(tile.Value, -1);
                }
            }
        }

        private void ChangeGuideLineColor(int _hitIdx, int _colorIdx)
        {
            Color color = default;
            if (_colorIdx == 0 || _colorIdx == 1) { color = Piano_Management.STAT_COLORS[0]; }
            else if (_colorIdx == 2 || _colorIdx == 3) { color = Piano_Management.STAT_COLORS[1]; }
            else if (_colorIdx == 4 || _colorIdx == 5) { color = Piano_Management.STAT_COLORS[2]; }
            else if (_colorIdx == 6 || _colorIdx == 7) { color = Piano_Management.STAT_COLORS[3]; }
            else { color = Color.white; }

            Piano_Management.Instance.P_Spawner.guideLines[_hitIdx].color = color;
        }
    }
}