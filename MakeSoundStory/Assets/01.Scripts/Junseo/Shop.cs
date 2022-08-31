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

    [SerializeField]
    private Button shopButton;


    // ---------------------------아이템 사용 관련 변수들 --------------------------
    [SerializeField]
    private GameObject useItemObj;

    [SerializeField]
    private Button ItemPanelButton;

    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    [SerializeField]
    private Button useItemButton;

    [SerializeField]
    private int itemIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        ShopStart();
        Test();
        UseItemSetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test() //보유량을 0으로 초기화하는 함수, 빌드때는 쓰지않을것
    {
        for(int i = 0; i < itemList.items.Length; i++)
        {
            itemList.items[i].reserve = 0;
        }
    }

    public void BuyItem(Item item)
    {
        if (item.reserve < 5)
        {
            buyPanel.SetActive(true);
            buyPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName + " x1\n 구매했습니다.";
            item.reserve++;
            //buyPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.itemSprite;
            buyPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
            buyPanel.SetActive(false);
                for (int i = 0; i < shopPanels.Length; i++)
                {
                    shopPanels[i].transform.GetChild(4).GetComponent<Text>().text = "보유량 : " + itemList.items[i].reserve.ToString() + "개";
                }
            });
        }
        else
        {
            buyPanel.SetActive(true);
            buyPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "아이템 보유량이 최대 개수를 넘었습니다.";
            //buyPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.itemSprite;
            buyPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                buyPanel.SetActive(false);
            });
        }
    }

    public void ShopStart()
    {
        shopButton.onClick.AddListener(() => { shopObj.SetActive(true);
            useItemObj.SetActive(false);
        });

        ItemPanelButton.onClick.AddListener(() =>
        {
            shopObj.SetActive(false);
            useItemObj.SetActive(true);
        });
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
                shopPanels[i].transform.GetChild(4).GetComponent<Text>().text = "보유량 : " + itemList.items[i].reserve.ToString() + "개";
                shopPanels[index].transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(itemList.items[index].itemName);
                    GameManager.instance.playerMoney -= itemList.items[index].price;
                    BuyItem(itemList.items[index]);
                });
            }
    }

    public void UseItemSetting()
    {
        ItemTextSetting(itemIndex);
        leftButton.onClick.AddListener(() =>
        {
            if(--itemIndex < 0)
            {
                itemIndex = itemList.items.Length - 1;
                ItemTextSetting(itemIndex);
            }
            else
            {
                ItemTextSetting(itemIndex); 
            }
        });
        rightButton.onClick.AddListener(() =>
        {
            if(++itemIndex > itemList.items.Length - 1)
            {
                itemIndex = 0;
                ItemTextSetting(itemIndex);
            }
            else
            {
                ItemTextSetting(itemIndex);
            }    
        });
    }

    public void ItemTextSetting(int index)
    { 
        useItemObj.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemList.items[index].itemName;
        useItemObj.transform.GetChild(2).gameObject.GetComponent<Text>().text = itemList.items[index].itemExplane;
        useItemObj.transform.GetChild(3).gameObject.GetComponent<Text>().text = "보유량 : " + itemList.items[index].reserve.ToString() + "개"; 
    }

}
