using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    public static BackGroundManager instance;

    [SerializeField]
    private Sprite[] backgroundSprites;

    [SerializeField]
    private GameObject[] backgroundPanels;

    [SerializeField]
    private GameObject backgroundParent;

    [SerializeField]
    private GameObject backgroundPrefab;

    private bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("백그라운드 매니저가 이미 있습니다.");
        }
        StartSetting();
    }

    public void StartSetting()
    {
        for(int i = 0; i < backgroundPanels.Length; i++)
        {
            backgroundPanels[i].GetComponent<Image>().sprite = backgroundSprites[i];
        }

    }

    public void BackGroundRightMove()
    {

        if (!isMove)
        {

            isMove = true;

            Destroy(backgroundPanels[0].gameObject);
            backgroundPanels[1].transform.DOMoveX(backgroundPanels[0].transform.position.x, 1f);
            backgroundPanels[2].transform.DOMoveX(backgroundPanels[1].transform.position.x, 1f).OnComplete(() =>
            {
                GameObject newBackgroundPanel = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newBackgroundPanel.transform.parent = backgroundParent.transform;
                newBackgroundPanel.transform.localScale = new Vector3(1, 1);
                newBackgroundPanel.transform.localPosition = new Vector3(1926, 0);


                backgroundPanels[0] = backgroundParent.transform.GetChild(0).gameObject;
                backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
                backgroundPanels[2] = backgroundParent.transform.GetChild(2).gameObject;
                isMove = false;
            });
        }
    }

    public void BackGroundLeftMove()  
    {
        if (!isMove)
        {
            backgroundPanels[2] = backgroundParent.transform.GetChild(0).gameObject;
            backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
            backgroundPanels[0] = backgroundParent.transform.GetChild(2).gameObject;
            isMove = true;
            Destroy(backgroundPanels[2].gameObject);
            backgroundPanels[0].transform.DOMoveX(backgroundPanels[1].transform.position.x, 1f);
            backgroundPanels[1].transform.DOMoveX(backgroundPanels[2].transform.position.x, 1f).OnComplete(() =>
            {
                GameObject newBackgroundPanel = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newBackgroundPanel.transform.parent = backgroundParent.transform;
                newBackgroundPanel.transform.localScale = new Vector3(1, 1);
                newBackgroundPanel.transform.localPosition = new Vector3(-1926, 0);


                backgroundPanels[2] = backgroundParent.transform.GetChild(0).gameObject;
                backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
                backgroundPanels[0] = backgroundParent.transform.GetChild(2).gameObject;
                isMove = false;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
