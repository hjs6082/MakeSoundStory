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

    public void ShopStart()
    {
        for(int i = 0; i < itemList.items.Length; i++)
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
            shopPanels[i].transform.GetChild(0).GetComponent<Text>().text = "#" + itemList.items[i].number;
            //dogamPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = dogamPanels[i].GetComponent<MyData>().myData.CharacterImg; <<아트가 없기때문에 아직 못넣었어요.
            shopPanels[i].transform.GetChild(2).GetComponent<Text>().text = itemList.items[i].itemName;
            shopPanels[i].transform.GetChild(3).GetComponent<Text>().text = itemList.items[i].itemExplane;
            shopPanels[i].transform.GetChild(4).GetComponent<Text>().text = "남은 수량 : " + itemList.items[i].maxamount.ToString() + "개";
            shopPanels[i].transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => 
            {
                GameManager.instance.playerMoney -= itemList.items[i].price; 
                itemList.items[i].amount++; 
            });
        }
    }
}
