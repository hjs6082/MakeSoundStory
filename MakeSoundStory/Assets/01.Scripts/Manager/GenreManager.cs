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
            Debug.Log("이미 장르매니저가 있습니다.");
        }
        AddGenre();

    }

    public void AddGenre()
    {
        GenreSO[] allGenres = Resources.LoadAll<GenreSO>("GenreSO");
        //for(int i = 0; i < allGenres.Length; i++)
        for(int i = 0; i < 10; i++)
        {
            genreList.Add(Resources.Load<GenreSO>($"GenreSO/GenreSO{i + 1}"));
        }
    }

    public void GenreSet(Transform genreParent)
    {
        int idx = 0;

        if (genreParent.childCount == 0)
        {
            for (int i = 0; i < genreList.Count; i++)
            {
                idx = i;

                GenreSO genreSO = genreList[idx];
                GameObject genreButton = Instantiate(genrePrefab, new Vector3(0, 0, 0), Quaternion.identity, genreParent);

                //genreButton.transform.SetParent(genreParent);
                genreButton.transform.localScale = new Vector3(1, 1, 1);
                genreButton.GetComponent<GenreData>().myData = genreSO;
                genreButton.transform.GetChild(0).GetComponent<Text>().text = genreSO.GenreName;

                genreButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    genreText.text = genreSO.GenreName;
                    GenreCheck(genreButton.GetComponent<GenreData>().myData);
                    
                    BPM_Management.Instance.Set_BPM_Dic(genreSO.GenreIndex);
                    BPM_Management.Instance.isSetting = true;
                    
                    genrePanel.SetActive(false);
                });
            }

        }
        // else
        // {
        //     for(int i = genreParent.childCount - 1; i >= 0; i--)
        //     {
        //         Destroy(genreParent.transform.GetChild(i).gameObject);
        //     } 
        // }

    }

    public void GenreCheck(GenreSO checkGenre)
    {
        for (int i = 0; i < StaffManager.instance.pickWorkStaffList.Count; i++)
        {
            if (StaffManager.instance.pickWorkStaffList[i].HateGenre.ToString() == checkGenre.GenreName)
            {
                waringText.gameObject.SetActive(true);
                waringText.text = "선택한 직원중 이 장르를 싫어하는 직원이 있습니다. 해당 직원의 능력치가 -5% 감소합니다.";
                waringText.color = new Color(255, 0, 0); 
            }
            else if(StaffManager.instance.pickWorkStaffList[i].FavoriteGenre.ToString() == checkGenre.GenreName)
            {
                waringText.text = "선택한 직원중 이 장르를 좋아하는 직원이 있습니다. 해당 직원의 능력치가 5% 증가합니다.";
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
