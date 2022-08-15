using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenreManager : MonoBehaviour
{
    public static GenreManager instance;

    [SerializeField]
    private GameObject genrePrefab;

    public List<GenreSO> genreList = new List<GenreSO>();

    [SerializeField]
    private GameObject genrePanel;

    [SerializeField]
    private Text genreText;

    [SerializeField]
    private Text waringText;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("�̹� �帣�Ŵ����� �ֽ��ϴ�.");
        }
        AddGenre();
    }

    public void AddGenre()
    {
        GenreSO[] allGenres = Resources.LoadAll<GenreSO>("GenreSO");
        for(int i = 0; i < allGenres.Length; i++)
        {
            genreList.Add(allGenres[i]);
        }
    }

    public void GenreSet(Transform genreParent)
    {
        if (genreParent.childCount == 0)
        {
            for (int i = 0; i < genreList.Count; i++)
            {
                GameObject genreButton = Instantiate(genrePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                genreButton.transform.SetParent(genreParent);
                //genreButton.transform.parent = genreParent;
                genreButton.transform.localScale = new Vector3(1, 1, 1);
                genreButton.GetComponent<GenreData>().myData = genreList[i];
                genreButton.transform.GetChild(0).GetComponent<Text>().text = genreList[i].GenreName;
                genreButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    genrePanel.SetActive(false);
                    genreText.text = genreButton.GetComponent<GenreData>().myData.GenreName;
                    GenreCheck(genreButton.GetComponent<GenreData>().myData);
                });
            }
        }
        else
        {
            for(int i = genreParent.childCount; i == 0; i--)
            {
                Destroy(genreParent.transform.GetChild(i).gameObject);
            } 
        }

    }

    public void GenreCheck(GenreSO checkGenre)
    {
        for (int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if (StaffManager.instance.pickWorkStaffList[i].HateGenre.ToString() == checkGenre.GenreName)
            {
                waringText.gameObject.SetActive(true);
                waringText.text = "������ ������ �� �帣�� �Ⱦ��ϴ� ������ �ֽ��ϴ�. �ش� ������ �ɷ�ġ�� -5% �����մϴ�.";
                waringText.color = new Color(255, 0, 0); 
            }
            else if(StaffManager.instance.pickWorkStaffList[i].FavoriteGenre.ToString() == checkGenre.GenreName)
            {
                waringText.text = "������ ������ �� �帣�� �����ϴ� ������ �ֽ��ϴ�. �ش� ������ �ɷ�ġ�� 5% �����մϴ�.";
                waringText.gameObject.SetActive(true);
                waringText.color = new Color(170, 255, 0);
            }
            else
            {
                waringText.gameObject.SetActive(false);
            }
        }
    }


}
