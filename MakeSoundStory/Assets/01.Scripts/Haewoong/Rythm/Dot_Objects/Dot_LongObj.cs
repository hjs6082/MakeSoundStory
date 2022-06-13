using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_LongObj : Dot_Object
{
    protected override Color dot_Color { get; set; }
    protected override SpriteRenderer dot_Renderer { get; set; }
    protected override Animator animator { get; set; }

    protected override void Awake()
    {
        InitValue();
    }

    protected override void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * 7.5f;

        if(transform.position.x <= -20.0f)
        {
            Destroy(gameObject);
        }
    }

    protected override void InitValue()
    {
        
    }

    protected override Sprite LoadImage()
    {
        Sprite dot_Image = null;

        return dot_Image;
    }
}
