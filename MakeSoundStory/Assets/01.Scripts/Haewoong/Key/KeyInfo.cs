using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Piano;

public class KeyInfo : MonoBehaviour
{
    private const string KEY_INFO_PATH = "Haewoong/KeyInfoSO/KeyInfoSO_";

    [Header("키 정보 관련")]
    public KeyInfoSO keyInfoSO = null;
    public Sprite keyImage => keyInfoSO.keyImage;
    public KeyCode keyCode => keyInfoSO.keyCode;

    [Header("키 설정 관련")]
    public Button keyBtn = null;

    private void Start()
    {
        InitValue();
    }

    private void InitValue()
    {
        keyInfoSO = Resources.Load<KeyInfoSO>(KEY_INFO_PATH + this.transform.name);

        keyBtn = GetComponent<Button>();
        keyBtn.onClick.AddListener(() => 
        {
            Piano_KeyMap.Instance.InitTile();
            if(Piano_KeyMap.Instance.curSelectedKey != this.keyCode)
            {
                SelectKey();
            }
        });

        if(Piano_KeyMap.Instance != null)
        {
            Piano_KeyMap.Instance.pianoTile_KeyInfo_List.Add(this);
            Piano_KeyMap.Instance.pianoTile_KeyImage_Dic.Add(this.keyCode, this.keyImage);
        }
    }

    public void SelectKey()
    {
        if(Piano_KeyMap.Instance != null)
        {
            Piano_KeyMap.Instance.curSelectedKey = this.keyCode;
        }
        
        this.transform.localScale = Vector2.one * 1.1f;
    }

    public void InitSize()
    {
        this.transform.localScale = Vector2.one;
    }
}
