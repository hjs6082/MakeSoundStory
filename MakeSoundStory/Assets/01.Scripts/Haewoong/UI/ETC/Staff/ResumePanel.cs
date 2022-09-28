
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResumePanel : MonoBehaviour
{
    public int repeatCount = 0;
    public int count = 0;
    public int maxCount = 0;
    public float tweenTime = 0.1f;
    public RectTransform[] loadingObjs = null;
    public Image background = null;

    public void InitValue()
    {
        repeatCount = 0;
        count = 0;
        maxCount = loadingObjs.Length - 1;

        background = this.GetComponent<Image>();
        background.color = Color.clear;

        for(int i = 0; i < loadingObjs.Length; i++)
        {
            Text txt = loadingObjs[i].GetComponent<Text>();

            txt.color = Color.clear;
        }

        OffPanel();
    }

    public void OnPanel()
    {
        this.gameObject.SetActive(true);
        background.DOColor(Color.white, 0.5f)
        .OnComplete(() => {
            LoadingDotSeq(() => OffPanel());
        });

        for(int i = 0; i < loadingObjs.Length; i++)
        {
            Text txt = loadingObjs[i].GetComponent<Text>();

            txt.DOColor(Color.black, 0.5f);
        }
    }

    public void OffPanel()
    {
        background.DOColor(Color.clear, 0.5f)
        .OnComplete(() => {
            repeatCount = 0;
            this.gameObject.SetActive(false);
        });

        for(int i = 0; i < loadingObjs.Length; i++)
        {
            Text txt = loadingObjs[i].GetComponent<Text>();

            txt.DOColor(Color.clear, 0.5f);
        }
    }

    private void LoadingDotSeq(Action _act = null)
    {
        RectTransform obj = loadingObjs[count];

        obj.DOAnchorPosY(obj.anchoredPosition.y + 25.0f, tweenTime)
        .OnComplete(() => {
            obj.DOAnchorPosY(obj.anchoredPosition.y - 25.0f, tweenTime)
            .OnComplete(() => {
                if(count < maxCount) { count++; }
                else { count = 0; repeatCount++; }

                if(repeatCount >= 2) { _act?.Invoke(); }
                else { LoadingDotSeq(_act); }
            });
        });
    }
}
