using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    public static BackGroundManager instance;

    [SerializeField]
    private GameObject backgroundParent;

    [SerializeField]
    private GameObject backgroundPrefab;

    public List<BackgroundSO> backgroundList = new List<BackgroundSO>();

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
        BackgroundSetting();
    }

    private void Update()
    {

    }

    public void BackgroundSetting()
    {
        BackgroundSO[] allBackgrounds = Resources.LoadAll<BackgroundSO>("BackgroundSO");
        for(int i = 0; i < allBackgrounds.Length; i++)
        {
            backgroundList.Add(allBackgrounds[i]);
            GameObject backPrefab = Instantiate(backgroundPrefab, backgroundParent.transform.position, Quaternion.identity);
            backPrefab.transform.parent = backgroundParent.transform;
            backPrefab.transform.localScale = new Vector3(1, 1, 1);
            backPrefab.AddComponent<BackgroundData>().myData = allBackgrounds[i]; 
            backPrefab.GetComponent<Image>().sprite = backPrefab.GetComponent<BackgroundData>().myData.MySprite;    
        }
    }

    public void ShowNowPlace()
    {

    }
}

    /*    [SerializeField]
        private Sprite[] backgroundSprites;

        [SerializeField]
        private GameObject[] backgroundPanels;

        [SerializeField]
        private GameObject backgroundParent;

        [SerializeField]
        private GameObject backgroundPrefab;

        private bool isMove;

       [SerializeField]
        private bool lastTouch = true;

        [SerializeField]
        int panelIndex = 2;



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
            backgroundPanels[0].transform.localPosition = new Vector3(-1926,0,0);
            backgroundPanels[1].transform.localPosition = new Vector3(0,0,0);
            backgroundPanels[2].transform.localPosition = new Vector3(1926,0,0 );
            isMove = true;

            Destroy(backgroundPanels[0].gameObject);
            backgroundPanels[1].transform.DOMoveX(backgroundPanels[0].transform.position.x, 1f);
            backgroundPanels[2].transform.DOMoveX(backgroundPanels[1].transform.position.x, 1f).OnComplete(() =>
            {
                GameObject newBackgroundPanel = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newBackgroundPanel.transform.parent = backgroundParent.transform;
                newBackgroundPanel.transform.localScale = new Vector3(1, 1);
                newBackgroundPanel.transform.localPosition = new Vector3(1926, 0);

                if (lastTouch == false)
                {
                    panelIndex++;
                    RightIndexCheck(newBackgroundPanel);
                    panelIndex++;
                    RightIndexCheck(newBackgroundPanel);
                    panelIndex++;
                    RightIndexCheck(newBackgroundPanel);
                }
                else
                {
                    panelIndex++;
                    RightIndexCheck(newBackgroundPanel);
                }


                backgroundPanels[0] = backgroundParent.transform.GetChild(0).gameObject;
                backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
                backgroundPanels[2] = backgroundParent.transform.GetChild(2).gameObject;
                isMove = false;
                lastTouch = true;
            });
        }
    }

    public void BackGroundLeftMove()  
    {
        Debug.Log("234");

        if (!isMove)
        {
            backgroundPanels[2] = backgroundParent.transform.GetChild(0).gameObject;
            backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
            backgroundPanels[0] = backgroundParent.transform.GetChild(2).gameObject;

            backgroundPanels[2].transform.localPosition = new Vector3(1926, 0, 0);
            backgroundPanels[1].transform.localPosition = new Vector3(0, 0, 0);
            backgroundPanels[0].transform.localPosition = new Vector3(-1926, 0, 0);

            Destroy(backgroundPanels[2].gameObject);
            backgroundPanels[0].transform.DOMoveX(backgroundPanels[1].transform.position.x, 1f);
            backgroundPanels[1].transform.DOMoveX(backgroundPanels[2].transform.position.x, 1f).OnComplete(() =>
            {
                GameObject newBackgroundPanel = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newBackgroundPanel.transform.parent = backgroundParent.transform;
                newBackgroundPanel.transform.localScale = new Vector3(1, 1);
                newBackgroundPanel.transform.localPosition = new Vector3(-1926, 0);

                if (lastTouch == true)
                {
                    panelIndex--;
                    IndexCheck(newBackgroundPanel);
                    panelIndex--;
                    IndexCheck(newBackgroundPanel);
                    panelIndex--;
                    IndexCheck(newBackgroundPanel);
                }
                else
                {
                    panelIndex--;
                    IndexCheck(newBackgroundPanel);

                }

                backgroundPanels[2] = backgroundParent.transform.GetChild(0).gameObject;
                backgroundPanels[1] = backgroundParent.transform.GetChild(1).gameObject;
                backgroundPanels[0] = backgroundParent.transform.GetChild(2).gameObject;
                isMove = false;
                lastTouch = false;
            });
        }
    }

    public void IndexCheck(GameObject backGroundPanel)
    {
        if (panelIndex < 0)
        {
            panelIndex = 4;
            backGroundPanel.GetComponent<Image>().sprite = backgroundSprites[panelIndex];
        }
        else
        {
            backGroundPanel.GetComponent<Image>().sprite = backgroundSprites[panelIndex];
        }

    }

    public void RightIndexCheck(GameObject backGroundPanel)
    {
        if (panelIndex <= 4)
        {

            backGroundPanel.GetComponent<Image>().sprite = backgroundSprites[panelIndex];
        }
        else
        {
            panelIndex = 0;
            backGroundPanel.GetComponent<Image>().sprite = backgroundSprites[panelIndex];

        }
    }*/
