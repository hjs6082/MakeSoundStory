using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject lookPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PanelSetting(lookPanel, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelSetting(lookPanel, false);
    }

    public void PanelSetting(GameObject panel, bool isLook)
    {
        panel.SetActive(isLook);
    }
}
