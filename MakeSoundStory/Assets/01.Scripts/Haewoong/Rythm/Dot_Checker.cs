using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Checker : MonoBehaviour
{
    private Dot_Controller dot_Ctrl = null;

    public List<Dot_Obj> hit_Dot = new List<Dot_Obj>();

    private void Awake()
    {
        dot_Ctrl = Dot_Management.Instance.GetController();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        dot_Ctrl.isCols = true;

        hit_Dot.Add(other.GetComponent<Dot_Obj>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dot_Ctrl.isCols = false;
        
        hit_Dot.Clear();
    }
}
