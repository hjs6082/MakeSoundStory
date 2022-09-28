using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    /*
    필요한 패널:
    -NPC 별 패널 (고용, 음악제작, 설정, 대출, 회사, 상점) -> StaffPanel 추상클래스 사용하면 될듯
    -이벤트 표시 패널
    -explain 패널

    */  
#region Values
    public static UIManagement instance = null;

    [Header("Main UI")]
    [SerializeField] private Text moneyText = null;
    [SerializeField] private Text dateText = null;

    public PanelPopUp staffPanelPopUp = null;
    public StaffPanel[] staffPanels = null;

    public bool isPanelOn = false;

#endregion

#region 기본 메시지 -> ex)Awake()
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitValue();

        InitStaffPanels();
    }

    private void Update()
    {
        
    }
#endregion

#region Initial 메소드
    private void InitValue()
    {
        MoneyTextUpdate();
    }

    private void InitButton()
    {

    }
#endregion

#region Main UI Canvas
    /// <summary>
    /// 
    /// </summary>
    public void MoneyTextUpdate()
    {
        StringBuilder sb = new StringBuilder().Clear();
        int curMoney = GameManager.instance.playerMoney;

        sb.Append("자본금 : ");
        sb.Append(curMoney.ToString());
        sb.Append("G");

        UIManagement.TextUpdate(moneyText, sb.ToString());
    }

#endregion

#region NPC
    ///<summary>
    /// 각각의 NPC 패널
    ///</summary>

    public void InitStaffPanels()
    {
        for(int i = 0; i < staffPanels.Length; i++)
        {
            staffPanels[i].init?.Invoke();
        }
    }
#endregion

#region Explain 패널
    
#endregion

#region Utility

    public static IEnumerator MethodDelay(Action _act, float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);

        _act?.Invoke();
    }

    public static void TextUpdate(Text _text, string _str)
    {
        _text.text = _str;
    }

    public static T GetOrAddComponent<T>(GameObject _obj) where T : UnityEngine.Component
    {
        T component = _obj.GetComponent<T>();

        if(component == null) component = _obj.AddComponent<T>();

        return component;
    }

    public T GetStaffPanel<T>() where T : StaffPanel
    {
        for(int i = 0; i < staffPanels.Length; i++)
        {
            if(staffPanels[i].GetType() == typeof(T))
            {
                return (T)staffPanels[i];
            }
        }

        return null;
    }
#endregion
}
