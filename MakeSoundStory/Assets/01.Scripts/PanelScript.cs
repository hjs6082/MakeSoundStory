using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject lookPanel;

    public enum PanelType
    {
        EnterAndExitPanel,   //마우스를 갓다대고 떼면 사라지는 패널
        ClickPanel           //클릭하면 계속 있는 패널
    }
    public PanelType panelType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelType == PanelType.EnterAndExitPanel)
        {
            PanelSetting(lookPanel, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelType == PanelType.EnterAndExitPanel)
        {
            PanelSetting(lookPanel, false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(panelType == PanelType.ClickPanel)
        {
            PanelSetting(lookPanel, true);
        }
    }

    public void PanelSetting(GameObject panel, bool isLook)
    {
        panel.SetActive(isLook);
    }
}
