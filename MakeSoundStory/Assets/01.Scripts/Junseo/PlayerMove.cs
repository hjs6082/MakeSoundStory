using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 position;
    public float speed = 1;

    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("@3");
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.y += speed * Time.deltaTime;
        }
        transform.position = position;
    }
}
