using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject shopObj;
    [SerializeField]
    private GameObject shopPrefab;

    [SerializeField]
    private GameObject buyPanel;

    [SerializeField]
    private ItemSO itemList;

    [SerializeField]
    private GameObject[] shopPanels;

    // Start is called before the first frame update
    void Start()
    {
        ShopStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyItem(Item item)
    {
        buyPanel.SetActive(true);
        buyPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName + " x1\n 구매했습니다.";
        //buyPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.itemSprite;
        buyPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            //ShopStart();
            buyPanel.SetActive(false);
        });
    }

    public void ShopStart()
    {
/*        if (shopObj.transform.childCount != 0)
        {
            for (int i = 0; i < shopObj.transform.childCount; i++)
            {
                Destroy(shopObj.transform.GetChild(i).gameObject);
            }
        }
        else
        {*/
            for (int i = 0; i < itemList.items.Length; i++)
            {
                GameObject shop = Instantiate(shopPrefab, new Vector3(0f, 0f), Quaternion.identity);
                shop.transform.parent = shopObj.transform;
                shop.transform.localScale = new Vector3(1f, 1f);
            }
            int shopListCount = shopObj.transform.childCount;
            shopPanels = new GameObject[shopListCount];
            for (int i = 0; i < shopListCount; i++)
            {
                shopPanels[i] = shopObj.transform.GetChild(i).gameObject;
            }
            for (int i = 0; i < shopPanels.Length; i++)
            {
                int index = i;
                shopPanels[i].transform.GetChild(0).GetComponent<Text>().text = "#" + itemList.items[i].number;
                //dogamPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = dogamPanels[i].GetComponent<MyData>().myData.CharacterImg; <<아트가 없기때문에 아직 못넣었어요.
                shopPanels[i].transform.GetChild(2).GetComponent<Text>().text = itemList.items[i].itemName;
                shopPanels[i].transform.GetChild(3).GetComponent<Text>().text = itemList.items[i].itemExplane;
                shopPanels[i].transform.GetChild(4).GetComponent<Text>().text = "남은 수량 : " + itemList.items[i].maxamount.ToString() + "개";
                shopPanels[index].transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(itemList.items[index].itemName);
                    GameManager.instance.playerMoney -= itemList.items[index].price;
                    BuyItem(itemList.items[index]);
                    itemList.items[index].maxamount--;
                    itemList.items[index].amount++;
                });
            }

        //}
    }


}
