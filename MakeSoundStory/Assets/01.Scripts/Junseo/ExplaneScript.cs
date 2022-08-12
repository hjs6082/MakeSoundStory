using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ExplaneScript : MonoBehaviour
{
    public static ExplaneScript instance;
    [SerializeField]
    private GameObject eventPanel;

    [SerializeField]
    private GameObject oxPanel;

    private void Awake()
    {
        instance = this;
    }
    public void notHumanEvent(string text, bool isOX)
    {
        if(isOX)
        {
            oxPanel.SetActive(true);
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
    }

    public void HumanEvent(StaffSO staff, string text, bool isOX)
    {
        if(isOX)
        {
            oxPanel.SetActive(true);
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f, eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            GameObject setStaff = Instantiate(staff.MySprite, eventPanel.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
            setStaff.transform.parent = eventPanel.transform.GetChild(0);
            setStaff.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 5;
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f);
        }
        if(!isOX)
        {
            eventPanel.SetActive(true);
            StartCoroutine(endEvent(5f,eventPanel));
            eventPanel.GetComponent<Button>().onClick.AddListener(() => { eventPanel.SetActive(false); });
            GameObject setStaff = Instantiate(staff.MySprite, eventPanel.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
            setStaff.transform.parent = eventPanel.transform.GetChild(0);
            setStaff.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 5;
            eventPanel.transform.GetChild(1).GetComponent<Text>().text = text;
            eventPanel.transform.DOLocalMoveX(519f, 1f); 
        }
    }

    IEnumerator endEvent(float time, GameObject falseObj)
    {
        yield return new WaitForSeconds(time);
        falseObj.SetActive(false);
    }
}
