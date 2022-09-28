using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class StaffResume : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    [Header("사용하는 변수")]
    private DrawPanel drawPanel = null;
    private Image background = null;
    public bool bSelected = false;

    [Header("스태프 이미지")]
    public StaffSO staffInfo = null;
    public RectTransform staffRectTrm = null;
    public GameObject staffObj = null;

    [Header("정보 Texts")]
    public Text nameText     = null;
    public Text levelText    = null;
    public Text favoriteText = null;
    public Text hateText     = null;
    public Text jobText      = null;
    public Text costText     = null;

    [Header("스탯 Texts")]
    public Text creativityText = null;
    public Text addictiveText  = null;
    public Text melodicText    = null;
    public Text popularityText = null;

    private void Awake()
    {
        drawPanel = this.transform.parent.GetComponentInParent<DrawPanel>();
        background = this.transform.GetComponent<Image>();
    }

    public void SetStaff(StaffSO _staffInfo)
    {
        staffInfo = _staffInfo;

        if(staffObj != null) { Destroy(staffObj); staffObj = null; }
        staffObj = Instantiate<GameObject>(staffInfo.StaffHeadPrefab, staffRectTrm);

        RectTransform staffRect = staffObj.GetComponent<RectTransform>();
        staffRect.anchoredPosition = new Vector2(3, -8);
        staffRect.localScale = new Vector2(0.7f, 0.7f);
        
        UIManagement.GetOrAddComponent<SortingGroup>(staffObj).sortingOrder = 50;

        SetTexts(staffInfo.StaffName, staffInfo.StaffLevel, staffInfo.FavoriteGenre.ToString(), staffInfo.HateGenre.ToString(), staffInfo.StaffJob,
                 staffInfo.Money, staffInfo.Creativity, staffInfo.Addictive, staffInfo.Melodic, staffInfo.Popularity);
    }

    private void SetTexts(string _name, int _level, string _favorite, string _hate, string _job,
                         int _cost, int _create, int _addict, int _melodic, int _popular)
    {
        nameText.text = "이름 : " + _name;
        levelText.text = "레벨 : " + _level;
        favoriteText.text = "좋아하는 장르 : " + _favorite;
        hateText.text = "싫어하는 장르 : " + _hate;
        jobText.text = "분야 : " + _job;
        costText.text = "계약금 : " + _cost.ToString() + "G";

        creativityText.text = "독창성 : " + _create;
        addictiveText.text = "중독성 : " + _addict;
        melodicText.text = "멜로디컬 : " + _melodic;
        popularityText.text = "대중성 : " + _popular;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        drawPanel.SelectStaff(this.staffInfo);

        bSelected = !bSelected;
        
        StateUpdate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        background.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!bSelected)
        {
            Color color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
            background.color = color;
        }
    }

    public void StateUpdate()
    {
        // 색 조정
        Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        color.a = (bSelected) ? 1.0f : 0.4f;
        background.color = color;
    }
}
