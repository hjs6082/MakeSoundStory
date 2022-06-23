using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class OpenPanels : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject movePanel;

    public enum type
    {
        Down,
        Left,
        Right
    }
    public type panelType;


    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (panelType)
        {
            case type.Down:
                movePanel.transform.DOLocalMoveY(-327, 0.7f);
                break;
            case type.Left:
                movePanel.transform.DOLocalMoveX(-897, 0.7f);
                break;
            case type.Right:
                movePanel.transform.DOLocalMoveX(880, 0.7f);
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        switch (panelType)
        {
            case type.Down:
                movePanel.transform.DOLocalMoveY(-566.9999f, 0.7f);
                break;
            case type.Left:
                movePanel.transform.DOLocalMoveX(-1051, 0.7f);
                break;
            case type.Right:
                movePanel.transform.DOLocalMoveX(1047f, 0.7f);
                break;
            default:
                break;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (panelType)
        {
            case type.Down:
                break;
            case type.Left:
                BackGroundManager.instance.BackGroundLeftMove();
                break;
            case type.Right:
                BackGroundManager.instance.BackGroundRightMove();
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
