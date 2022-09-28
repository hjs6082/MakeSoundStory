using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatInfo : MonoBehaviour
{
    private const int MAX_STAT = 100;
    private const float DEFAULT_WIDTH = 225.0f;

    [SerializeField] private Text curStatText = null;
    [SerializeField] private Image frontGuage = null;
    [SerializeField] private Image backGuage = null;

    public eStat ownStat = default;
    public StaffSO ownSO = null;

    private int curStat = 0;
    private Vector2 frontOffset = default;
    private Vector2 backOffset = default;

    public void InitValue(StaffSO _staffSO)
    {
        ownSO = _staffSO;

        frontOffset = new Vector2(0, 30);
        backOffset = new Vector2(0, 30);

        InitGuage();
    }

    public void InitGuage()
    {
        GuageUpdate();
        StatTextUpdate();
    }

    public void GuageUpdate()
    {
        SetCurStat();

        print((float)curStat / (float)MAX_STAT);
        frontOffset.x = ((float)curStat / (float)MAX_STAT) * DEFAULT_WIDTH;

        frontGuage.rectTransform.sizeDelta = frontOffset;
    }

    public void BackGuageUpdate(int _level)
    {
        SetCurStat();

        curStat += 5 * _level;

        backOffset.x = ((float)curStat / (float)MAX_STAT) * DEFAULT_WIDTH;

        backGuage.rectTransform.sizeDelta = backOffset;
    }

    private void SetCurStat()
    {
        switch(ownStat)
        {
            case eStat.CREATE: { curStat = ownSO.Creativity; }
            break;
            case eStat.ADDICT: { curStat = ownSO.Addictive; }
            break;
            case eStat.MELODI: { curStat = ownSO.Melodic; }
            break;
            case eStat.POPULA: { curStat = ownSO.Popularity; }
            break;
        }
    }

    private void StatTextUpdate()
    {
        curStatText.text = curStat.ToString();
    }
}
