using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class Piano_Tile : MonoBehaviour
    {
        public Image tileImage = null;
        public Image keyImage = null;

        public void ChangeTileColor(bool _bPress)
        {
            tileImage.color = (_bPress) ? Color.gray : Color.white;
        }
    }
}