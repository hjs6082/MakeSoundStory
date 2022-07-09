using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    
    public float zoomSpeed = 10.0f;
    private float tempValue;

    private bool drag = false;

    private void Start()
    {
        resetCamera = Camera.main.transform.position;
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if(drag)
        {
            Camera.main.transform.position = origin - difference;
            Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, -5, 5),
            Mathf.Clamp(Camera.main.transform.position.y, -2, 2), transform.position.z);
        }

        if(Input.GetMouseButton(1))
        {
            Camera.main.transform.position = resetCamera;
        }
    }
    private void Update()
    {
        zoom();
    }

    private void zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (mainCamera.orthographicSize <= 2.67f && scroll > 0)
        {
            tempValue = mainCamera.orthographicSize;
            mainCamera.orthographicSize = tempValue; // maximize zoom in
        }

        else if (mainCamera.orthographicSize >= 7.03f && scroll < 0)
        {
            tempValue = mainCamera.orthographicSize;
            mainCamera.orthographicSize = tempValue; // maximize zoom out
        }
        else
        {
            mainCamera.orthographicSize -= scroll * 0.5f;
        }
    }
}
