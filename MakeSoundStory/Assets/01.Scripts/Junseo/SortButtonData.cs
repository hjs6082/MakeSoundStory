using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortButtonData : MonoBehaviour
{
    [SerializeField]
    private GameObject sortButtonParent;
    public enum Sort
    {
        creativity, 
        addictive,  
        melodic,   
        popularity,
        level
    }
    public Sort sortData;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.StaffSort(this.gameObject, sortButtonParent); });
    }
}
