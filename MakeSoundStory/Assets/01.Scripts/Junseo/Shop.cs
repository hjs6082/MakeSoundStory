using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    private Button xButton;


    // ---------------------------������ ��� ���� ������ --------------------------
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

    // ���� ������ �̱� ���� ����
    [SerializeField]
    private GameObject gachaPanel;

    [SerializeField]
    private Image randomImage;

    [SerializeField]
    private Text randomText;

    [SerializeField]
    private Slider randomSlider;

    [SerializeField]
    private Button gachaEndButton;

    [SerializeField]
    private Button randomItemButton;

    private bool isGacha;


    // Start is called before the first frame update
    void Start()
    {
        ShopStart();
        Test();
        panelOff();
        UseItemSetting();
    }

    // Update is called once per frame
    void Update()
    {
        GachaSlider();
    }


    public void panelOff()
    {
        xButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            //Camera.main.GetComponent<CameraSetting>().enabled = true;
            UIManager.instance.staffTalkPanel.SetActive(false);
            StaffManager.instance.RunNpc();
        });
    }

    public void Test() //�������� 0���� �ʱ�ȭ�ϴ� �Լ�, ���嶧�� ����������
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
            buyPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName + " x1\n �����߽��ϴ�.";
            item.reserve++;
            //buyPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.itemSprite;
            buyPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
            buyPanel.SetActive(false);
                for (int i = 0; i < shopPanels.Length - 1; i++)
                {
                    shopPanels[i + 1].transform.GetChild(4).GetComponent<Text>().text = "������ : " + itemList.items[i].reserve.ToString() + "��";
                }
            });
        }
        else
        {
            buyPanel.SetActive(true);
            buyPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "������ �������� �ִ� ������ �Ѿ����ϴ�.";
            //buyPanel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.itemSprite;
            buyPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                buyPanel.SetActive(false);
            });
        }
    }

    public void ShopStart()
    {
        randomItemButton.onClick.AddListener(() =>
        {
            if (GameManager.instance.playerMoney > 2000)
            {
                GameManager.instance.playerMoney -= 2000;
                gachaPanel.SetActive(true);
                isGacha = true;
                RandomItemPick();
            }
            else
            {
                UIManager.instance.ShowExplane("���� �����մϴ�.");
            }
        });
        shopButton.onClick.AddListener(() =>
        {
            shopObj.SetActive(true);
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
        for (int i = 1; i < shopPanels.Length; i++)
        {
            int index = i - 1;
            shopPanels[i].transform.GetChild(0).GetComponent<Text>().text = "#" + itemList.items[index].number;
            //dogamPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = dogamPanels[i].GetComponent<MyData>().myData.CharacterImg; <<��Ʈ�� ���⶧���� ���� ���־����.
            shopPanels[i].transform.GetChild(2).GetComponent<Text>().text = itemList.items[index].itemName;
            shopPanels[i].transform.GetChild(3).GetComponent<Text>().text = itemList.items[index].itemExplane;
            shopPanels[i].transform.GetChild(4).GetComponent<Text>().text = "������ : " + itemList.items[index].reserve.ToString() + "��";
            shopPanels[i].transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = itemList.items[index].price + "G";
            shopPanels[i].transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log(itemList.items[index].itemName);
                if (GameManager.instance.playerMoney > itemList.items[index].price)
                {
                    GameManager.instance.playerMoney -= itemList.items[index].price;
                    BuyItem(itemList.items[index]);
                }
                else
                {
                    UIManager.instance.ShowExplane("���� �����մϴ�.");
                }
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
        if (index == 0 || index == 1 || index == 2)
        {
            useItemObj.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemList.items[index].itemName;
            useItemObj.transform.GetChild(2).gameObject.GetComponent<Text>().text = itemList.items[index].itemExplane;
            useItemObj.transform.GetChild(3).gameObject.GetComponent<Text>().text = "������ : " + itemList.items[index].reserve.ToString() + "��";
            useItemObj.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            useItemObj.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemList.items[index].itemName;
            useItemObj.transform.GetChild(2).gameObject.GetComponent<Text>().text = itemList.items[index].itemExplane;
            useItemObj.transform.GetChild(3).gameObject.GetComponent<Text>().text = "������ : " + itemList.items[index].reserve.ToString() + "��";
            useItemObj.transform.GetChild(4).gameObject.SetActive(true);    
        }
    }

    public Item RandomItem()
    { 
        int randomIndex = Random.Range(0, itemList.items.Length);
        return itemList.items[randomIndex];
    }

    public void RandomItemPick()
    {
        gachaEndButton.onClick.AddListener(() =>
        {
            gachaPanel.SetActive(false);
            randomText.text = "";
            for (int i = 0; i < shopPanels.Length - 1; i++)
            {
                shopPanels[i + 1].transform.GetChild(4).gameObject.GetComponent<Text>().text = "������ : " + itemList.items[i].reserve.ToString() + "��";
            }
        });
        StartCoroutine(RandomImage());
        randomText.GetComponent<Text>().DOText("���� �������� �̴����Դϴ�..", 5f).OnComplete(() =>
        {
            gachaEndButton.gameObject.SetActive(true);
            Item getItem = RandomItem();
            randomImage.sprite = getItem.itemSprite;
            randomText.text = getItem.itemName + " x1";
            getItem.reserve++;
        });
    }

    public void GachaSlider()
    {
        if (isGacha)
        {
            randomSlider.value += 0.2f * Time.deltaTime;
            if(randomSlider.value == 1f)
            {
                isGacha = false;
            }
        }
    }


    IEnumerator RandomImage()//Sprite[] imageList, Image randomImage, Text randomText)
    {
        Debug.Log("@34");
        for (int i = 0; i < itemList.items.Length; i++)
        {
            randomImage.sprite = itemList.items[i].itemSprite;
            yield return new WaitForSeconds(1f);
        }
    }

}
