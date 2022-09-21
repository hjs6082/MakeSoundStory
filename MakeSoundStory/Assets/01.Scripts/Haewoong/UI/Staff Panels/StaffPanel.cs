using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StaffPanel : MonoBehaviour
{
    public abstract Action init { get; set; }
    public abstract Button exitButton { get; set; }

    protected abstract void Update();
    protected abstract void TextUpdate();

    protected virtual void Awake()
    {
        init += () => InitValue();
        
        exitButton.onClick.AddListener(() => OffPanel());
    }

    protected virtual void InitValue()
    {
        return;
    }

    public virtual void OnPanel()
    {
        if(UIManagement.instance.isPanelOn)
        {
            return;
        }

        UIManagement.instance.isPanelOn = true;
        this.gameObject.SetActive(true);
    }

    public virtual void OffPanel()
    {
        UIManagement.instance.isPanelOn = false;
        this.gameObject.SetActive(false);
    }
    
    public virtual void Warning()
    {
        return;
    }
}
