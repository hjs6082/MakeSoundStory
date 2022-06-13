using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_NormalObj : Dot_Object, IBezier
{
    private const string IMAGE_PATH = "Haewoong/MakeImage/Button ";

    public KeyCode keyCode = KeyCode.None;
    public bool isBezier = false;
    
    protected override Color          dot_Color    { get; set; } = default;
    protected override SpriteRenderer dot_Renderer { get; set; } = null;
    protected override Animator       animator     { get; set; } = null;

    [Header("베지어 곡선 관련")]
    [SerializeField, Range(0, 1)]
    private float t = 0.0f;
    private List<Vector2> point = null;

    public float speed = 2.0f;
    public float radiusA = 0.55f;
    public float radiusB = 0.45f;
    public GameObject dot = null;
    public Vector3 arrival_Point = default;

    protected override void Awake()
    {
        InitValue();
    }

    protected override void Update()
    {
        if (!isBezier)
        {
            transform.position += Vector3.left * Time.deltaTime * 7.5f;

            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(t >= 1.0f)
            {
                t = 0.0f;
                point.Clear();
                Destroy(dot);
                return;
            }

            t += Time.deltaTime * speed;

            dot.transform.position = MoveBezier();
        }
    }

    protected override void InitValue()
    {
        animator = GetComponent<Animator>();
        dot_Renderer = GetComponent<SpriteRenderer>();
        dot_Renderer.sprite = LoadImage();
        dot_Renderer.color = dot_Color;

        t = 0.0f;
        point = new List<Vector2>();
        dot = this.gameObject;
    }

    protected override Sprite LoadImage()
    {
        Sprite dot_Image = null;
        StringBuilder sb = new StringBuilder();

        sb.Clear();
        sb.Append(IMAGE_PATH);
        switch (keyCode)
        {
            case KeyCode.UpArrow:    { sb.Append("Up");    dot_Color = Color.white;   } break;
            case KeyCode.DownArrow:  { sb.Append("Down");  dot_Color = Color.magenta; } break;
            case KeyCode.LeftArrow:  { sb.Append("Left");  dot_Color = Color.red;     } break;
            case KeyCode.RightArrow: { sb.Append("Right"); dot_Color = Color.yellow;  } break;
            default: break;
        }

        dot_Image = Resources.Load<Sprite>(sb.ToString());

        return dot_Image;
    }

    protected override void MoveDot()
    {
        base.MoveDot();
    }

#region IBezier 인터페이스 구현
    public void SetObjects(Vector2 _vec)
    {
        arrival_Point = _vec;

        point.Add(dot.transform.position);
        point.Add(SetRandomBezierPointP2(dot.transform.position));
        point.Add(SetRandomBezierPointP3(arrival_Point ));
        point.Add(arrival_Point);
    }

    public Vector2 MoveBezier()
    {
        Vector2 bezier = new Vector2(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y)
        );

        Debug.LogFormat("bezier {0}", bezier);

        return bezier;
    }

    public float FourPointBezier(float a, float b, float c, float d)
    {
        float fourPoint = Mathf.Pow(1.0f - t, 3f) * a
                        + Mathf.Pow(1.0f - t, 2f) * 3 * t * b
                        + Mathf.Pow(t, 2f) * 3 * (1 - t) * c
                        + Mathf.Pow(t, 3f) * d;

        return fourPoint;
    }

    public Vector2 SetRandomBezierPointP2(Vector2 _origin)
    {
        Vector2 randPointA = new Vector2(
            radiusA * Mathf.Cos(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.x,
            radiusA * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.y
        );

        return randPointA;
    }

    public Vector2 SetRandomBezierPointP3(Vector2 _origin)
    {
        Vector2 randPointB = new Vector2(
            radiusB * Mathf.Cos(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.x,
            radiusB * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.y
        );

        return randPointB;
    }
    #endregion
}