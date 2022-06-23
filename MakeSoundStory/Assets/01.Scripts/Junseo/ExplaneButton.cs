using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplaneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private void OnMouseEnter()
    {
       
    }

    private void OnMouseExit()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.ExplaneSetting(this.gameObject.GetComponent<StaffData>().myStaffData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.ExplaneExit();
    }
}
