using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private readonly Vector3 START_POS = new Vector3(-6.0f, -5.0f, 0.0f);

    [Range(5, 10), SerializeField]
    private float speed = 5.0f;
    [Range(30, 120), SerializeField]
    private int targetFrame = 60;
    
    private Transform player = null;
    private Transform cam = null;
    private Vector3 moveOffset = default;

    private void Awake()
    {
        FrameSetting();
        InitValue();
    }

    private void FixedUpdate()
    {
        if(Input.anyKey && !UIManagement.instance.isPanelOn)
        {
            PlayerControl();
        }
    }

    private void InitValue()
    {
        player = this.transform;
        cam = Camera.main.transform;

        player.position = START_POS;
        cam.position = player.position + new Vector3(0.0f, 0.0f, -10.0f);

        moveOffset = new Vector3();
    }

    // ���Ŀ� GameManager�� �ű� ����
    private void FrameSetting()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrame;
    }

    private void PlayerControl()
    {
        float offsetX = 0;
        float offsetY = 0;
        float speedOffset = Time.fixedDeltaTime * speed;

        offsetX = Input.GetAxis("Horizontal") * speedOffset;
        offsetY = Input.GetAxis("Vertical") * speedOffset;

        moveOffset.x = offsetX;
        moveOffset.y = offsetY;

        LockArea(ref moveOffset.x, ref moveOffset.y);

        player.position += moveOffset;
        cam.position += moveOffset;
    }

    private void LockArea(ref float _x, ref float _y)
    {
        Vector2 dirX = new Vector2(0, 0);
        Vector2 dirY = new Vector2(0, 0);

        dirX.x = (_x > 0) ? 1.0f : -1.0f;
        dirY.y = (_y > 0) ? 1.0f : -1.0f;

        RaycastHit2D hitX = Physics2D.Raycast(player.position, dirX, 8.0f);
        RaycastHit2D hitY = Physics2D.Raycast(player.position, dirY, 3.0f);

        Debug.DrawRay(player.position, dirX, Color.red, 8.0f);
        Debug.DrawRay(player.position, dirY, Color.red, 3.6f);

        if(hitX.transform != null && hitX.transform.tag.Equals("MapBorder")) { _x = 0.0f; }
        if(hitY.transform != null && hitY.transform.tag.Equals("MapBorder")) { _y = 0.0f; }
    }
}
