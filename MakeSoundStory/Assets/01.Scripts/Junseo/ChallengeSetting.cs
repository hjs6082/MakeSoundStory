/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSetting : MonoBehaviour
{
    [SerializeField] private GameObject chalObj;
    [SerializeField] private GameObject chalPrefab;
    [SerializeField] private GameObject[] chalObjs;

    //[SerializeField] private Challenge chals;

    private void Start()
    {
        ChallengeStart();
    }

    public void ChallengeStart()
    {
        for(int i = 0; i < chals.challenges.Length; i++)
        {
            GameObject chal = Instantiate(chalPrefab, new Vector3(0f, 0f), Quaternion.identity);
            chal.transform.parent = chalObj.transform;
            chal.transform.localScale = new Vector3(1f, 1f);
        }
        int chalCount = chalObj.transform.childCount;
        chalObjs = new GameObject[chalCount];
        for (int i = 0; i < chalCount; i++)
        {
            chalObjs[i] = chalObj.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < chalObjs.Length; i++)
        {
            //dogamPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = dogamPanels[i].GetComponent<MyData>().myData.CharacterImg; <<��Ʈ�� ���⶧���� ���� ���־����.
            chalObjs[i].gameObject.GetComponent<ChallengeData>().myChallenge = chals.challenges[i];
            chalObjs[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "#" + chals.challenges[i].challengeNumber;
            chalObjs[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = chals.challenges[i].challengeName;
            switch (chals.challenges[i].challengeType)
            {       
                case ChallengeInfo.type.staff:
                    chalObjs[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = chals.challenges[i].needValue + "���� �������� ����ϼ���.";
                    break;
                case ChallengeInfo.type.gold:
                    chalObjs[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = chals.challenges[i].needValue + "���� ������ ��������.";
                    break;
                case ChallengeInfo.type.music:
                    chalObjs[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = chals.challenges[i].needValue + "���� ���� �߸��ϼ���."; 
                    break;
                default:
                    break;
            }
            chalObjs[i].transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = chals.challenges[i].getGold + "G"; 

        }

    }
}
*/