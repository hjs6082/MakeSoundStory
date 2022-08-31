using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piano;

public class LineMove : MonoBehaviour
{
    private const float END_Y_POS = 535.0f;
    
    private Transform lineTrm = null;
    private RectTransform lineRectTrm = null;

    private Vector3 moveOffset = new Vector3(0.0f, 0.0f, 0.0f);
    
    private bool isMoving = false;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        Move();
    }

    private void InitValue()
    {
        lineTrm = this.transform;
        lineRectTrm = lineTrm.GetComponent<RectTransform>();

        isMoving = true;
    }

    private void Move()
    {
        if(isMoving)
        {
            float y = lineRectTrm.anchoredPosition.y;
            Mathf.Lerp(y, END_Y_POS, Piano_Management.Instance.delayTime);

            moveOffset.y = -y;
            lineRectTrm.anchoredPosition = moveOffset;

            if(END_Y_POS - y <= 0.00001f)
            {
                isMoving = false;
                Destroy(lineTrm.gameObject);
            }
        }
    }
}