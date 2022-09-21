using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

#region ��� ������ ����
public class AuctionItem
{
    public int index;
    public string musicName;
    public string madeOffice;
    public enum Genre 
    {
        ��,
        �˾غ�,
        ����,
        Ʈ��,
        �չ�,
        �߶��,
        Ʈ��Ʈ,
        ��,
        ��ũ,
        ��,
    }
    public enum Grade
    {
        normal,
        rare,
        unique,
        god
    }
    public Genre genre;
    public Grade grade;

    public AuctionItem(int index, string musicName, string madeOffice)
    {
        this.index = index;
        this.musicName = musicName;
        this.madeOffice = madeOffice;
        this.genre = (Genre)Random.Range(0, 9);
        this.grade = (Grade)Random.Range(0, 3);  
    }
}
#endregion


public class Auction : MonoBehaviour
{
    public List<AuctionItem> auctionItems = new List<AuctionItem>();  

    public GameObject auctionPanel;
    public GameObject bubblePrefab;
    public Transform[] buyUserPosition;
    public Text musicName;
    public Text genreName;
    public Text musicGold;
    public Text madeOffice;
    public Text nowGold;

    public Slider goldSlider;

    public Button bidButton; //��� ���� ��ư

    public int gold = 500;
    public int maxGold;

    public bool isAuction = false;

    public bool isPlrBid = false; //�������� �÷��̾ ����������
    public bool isAIBid = false;  //AI�� ����������.


    public void Start()
    {
        ItemParsing(Load(), auctionItems);
        UISetting(RandomItem()); 
    }

    public void Update()
    {
        if (isAuction)
        {
            nowGold.text = "���� ���� : "  + gold + "G";
            goldSlider.value -= 0.1f * Time.deltaTime;
        }
    }

    public void ItemParsing(JsonData itemData, List<AuctionItem> auctionItem) //������ �Ľ����� ���������� �ҷ��´�.
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            string tempIndex = itemData[i][0].ToString();
            string tempMusicName = itemData[i][1].ToString();
            string tempMadeOffice = itemData[i][2].ToString();

            int index = int.Parse(tempIndex); 
            AuctionItem parseItem = new AuctionItem(index, tempMusicName, tempMadeOffice);
            auctionItem.Add(parseItem);
        }

        foreach (var item in auctionItems)
        {
            Debug.Log(item.index + " " + item.musicName + " " + item.madeOffice + " " + item.genre + " " + item.grade);  
        }
    }

    public AuctionItem RandomItem() // ���� �������� �̴´�.
    {
        int randomRange = Random.Range(0, auctionItems.Count);
        return auctionItems[randomRange];
    }


    public void UISetting(AuctionItem nowItem) // ���� �������� ������ UI�� �����Ѵ�.
    {
        bidButton.onClick.AddListener(() => { Bid(); });
        musicName.text = "���� : " + nowItem.musicName;
        genreName.text = "�帣 : " + nowItem.genre.ToString();
        madeOffice.text = "���ۻ� : " + nowItem.madeOffice;
        nowGold.text = "���� ���� : " + 500 + "G";

        switch (nowItem.grade)
        {
            case AuctionItem.Grade.normal:
                maxGold = Random.Range(10, 15) * 100;
                break;
            case AuctionItem.Grade.rare:
                maxGold = Random.Range(30, 50) * 100;
                break;
            case AuctionItem.Grade.unique:
                maxGold = Random.Range(50, 70) * 100;
                break;
            case AuctionItem.Grade.god:
                maxGold = Random.Range(70, 10) * 100;
                break; 
            default:
                break;
        }
        musicGold.text = "���� ��ġ : " + maxGold + "G";
        isAuction = true;
        int randomGold = (Random.Range(maxGold - 500, maxGold + 500) / 100) * 100;
        StartCoroutine(AIBid(randomGold));
    }

    public void Bid() // ������ư ������ 
    {
        if (GameManager.instance.playerMoney >= gold + 500)
        {
            if (!isPlrBid)
            {
                PlrBid();
            }
            else
            {
                GameManager.instance.playerMoney += gold; 
                PlrBid();
            }
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }

    public void PlrBid() //�÷��̾ �����Ҷ�
    {
        gold += 500;
        GameManager.instance.playerMoney -= gold;
        nowGold.text = "���� ���� : " + gold + "G";
        goldSlider.value = 1f;
        isAIBid = false;
        isPlrBid = true; 
    } 

    public IEnumerator AIBid(int betGold)
    {
        Debug.Log(betGold);
        if(gold < betGold)
        {
            if (isPlrBid)
            {
                isPlrBid = false;
                GameManager.instance.playerMoney += gold; 
                gold += 500;
                nowGold.text = "���� ���� : " + gold + "G";
                goldSlider.value = 1f;
                isAIBid = true;
            }
            else
            {
                gold += 500;
                nowGold.text = "���� ���� : " + gold + "G";
                goldSlider.value = 1f;
                isAIBid = true; 
            }
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(AIBid(betGold));  
    } 

    public IEnumerator BidText(GameObject bidBubble, Text bidText, Transform spawnTrm) //���� �޽���
    {
        GameObject bidObj = Instantiate(bidBubble, spawnTrm);
        gold += 500;
        bidObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = gold + "G";

        yield return new WaitForSeconds(1.5f);
        Destroy(bidObj);
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Data/musicData.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }
}
