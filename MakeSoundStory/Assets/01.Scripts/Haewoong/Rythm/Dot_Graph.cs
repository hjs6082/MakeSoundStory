using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot_Graph : MonoBehaviour
{
    [SerializeField] private GameObject block_Prefab = null;
    public Transform[]  block_Parents = null;

    public void AddBlock(KeyCode _keyCode, int _idx)
    {
        Color blockColor = default;
        GameObject block = Instantiate<GameObject>(block_Prefab, block_Parents[_idx]);
        
        switch(_keyCode)
        {
            case KeyCode.UpArrow:    { blockColor = Color.white;  } break;
            case KeyCode.DownArrow:  { blockColor = Color.blue;   } break;
            case KeyCode.LeftArrow:  { blockColor = Color.red;    } break;
            case KeyCode.RightArrow: { blockColor = Color.yellow; } break;
            default: break;
        }

        block.GetComponent<Image>().color = blockColor;
    }
}
