using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    void Update()
    {
        this.transform.position += Vector3.down * Time.deltaTime * 10;
    }
}
