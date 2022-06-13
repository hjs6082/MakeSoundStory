using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Checker : MonoBehaviour
{
    private Dot_Controller dot_Ctrl = null;

    public List<Dot_NormalObj> hit_Dot = new List<Dot_NormalObj>();

    private void Awake()
    {
        dot_Ctrl = Dot_Management.Instance.GetController();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        dot_Ctrl.isCols = true;

        hit_Dot.Add(other.GetComponent<Dot_NormalObj>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dot_Ctrl.isCols = false;
        
        for(int i = 0; i < hit_Dot.Count; i++)
        {
            Dot_Management.Instance.GetSpawner().dotList.Remove(hit_Dot[i]);
        }
        
        hit_Dot.Clear();
    }
}
