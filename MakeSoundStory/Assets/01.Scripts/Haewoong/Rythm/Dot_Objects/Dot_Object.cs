using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dot_Object : MonoBehaviour
{
    private float moveSpeed = 7.5f;

    protected abstract Color          dot_Color    { get; set; }
    protected abstract SpriteRenderer dot_Renderer { get; set; }
    protected abstract Animator       animator     { get; set; }

    protected abstract void Awake();
    protected abstract void Update();
    protected abstract void InitValue();
    protected abstract Sprite LoadImage();

    protected virtual void MoveDot()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;

        if(transform.position.x <= -15.0f)
        {
            Destroy(gameObject);
        }
    }
}
