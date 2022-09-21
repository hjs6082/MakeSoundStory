using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPopUp : MonoBehaviour
{
    public Stack<GameObject> currentOnPanel = new Stack<GameObject>();
    public List<GameObject> test = new List<GameObject>();

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    public void PushPanelStack(GameObject _panel)
    {
        currentOnPanel.Push(_panel);
        test.Add(_panel);
    }

    public void PopPanelStack()
    {
        currentOnPanel.Pop().SetActive(false);
        test.RemoveAt(test.Count - 1);
    }

    public void EmptyPanelStack()
    {
        for(int i = 0; i < currentOnPanel.Count; i++)
        {
            currentOnPanel.Pop().SetActive(false);
        }
    }
}
