using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyData : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public StaffSO myStaff;

    private bool isSelect = false;

    [SerializeField]
    private Button selectButton;

    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        selectButton.onClick.AddListener(() => { SelectStaff(); });
        exitButton.onClick.AddListener(() => { UIManager.instance.staffGachaPanel.SetActive(false); });
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelect == false)
        {
            if (StaffManager.instance.isSelectStaff < 1)
            {
                Image image = this.gameObject.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 255f;
                image.color = tempColor;
                isSelect = true;
                StaffManager.instance.isSelectStaff++;
            }
        }
        else if(isSelect == true)
        {
            Image image = this.gameObject.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.42f;
            image.color = tempColor;
            isSelect = false;
            StaffManager.instance.isSelectStaff--;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image image = this.gameObject.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 255f;
        image.color = tempColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelect)
        {
            Image image = this.gameObject.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.42f;
            image.color = tempColor;
        }
    }

    public void SelectStaff()
    {
        if (isSelect == true)
        {
            StaffManager.instance.workStaffList.Add(myStaff);

            StaffManager.instance.pickStaffList.Remove(myStaff); //삭제될수도?

            UIManager.instance.SelectStaff(this.gameObject);
        }
    }

  
}
