using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Info : MonoBehaviour
{
    [SerializeField] private StaffPanel ownPanel = null;

    private void OnMouseDown()
    {
        ownPanel.OnPanel();
    }
}
