using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Bezier : MonoBehaviour
{
    private List<Vector2> point = new List<Vector2>();

    [SerializeField]
    [Range(0, 1)]
    private float t = 0;

    public float speed = 5.0f;
    public float radiusA = 0.55f;
    public float radiusB = 0.45f;

    public bool isPlaying = false;
    public GameObject master = null;
    public Vector3 objectPos;

    private void Start()
    {

    }

    private void Update()
    {
        if (isPlaying)
        {
            if (t >= 1)
            {
                t = 0.0f;
                point.Clear();
                isPlaying = false;
                return;
            };

            t += Time.deltaTime * speed;

            transform.position = MoveBezier();
        }
    }

    public void SetObjects(ref GameObject _obj, Vector2 _vec)
    {
        master = _obj;
        objectPos = _vec;

        point.Add(master.transform.position);
        point.Add(SetRandomBezierPointP2(master.transform.position));
        point.Add(SetRandomBezierPointP3(objectPos));
        point.Add(objectPos);

        Debug.LogFormat("point 0 : {0}", point[0]);
        Debug.LogFormat("point 1 : {0}", point[1]);
        Debug.LogFormat("point 2 : {0}", point[2]);
        Debug.LogFormat("point 3 : {0}", point[3]);

        isPlaying = true;
    }

    private Vector2 MoveBezier()
    {
        Vector2 bezier = new Vector2(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y)
        );

        Debug.LogFormat("bezier {0}", bezier);

        return bezier;
    }

    private float FourPointBezier(float a, float b, float c, float d)
    {
        float fourPoint = Mathf.Pow(1.0f - t, 3f) * a
                        + Mathf.Pow(1.0f - t, 2f) * 3 * t * b
                        + Mathf.Pow(t, 2f) * 3 * (1 - t) * c
                        + Mathf.Pow(t, 3f) * d;

        return fourPoint;
    }

    private Vector2 SetRandomBezierPointP2(Vector2 _origin)
    {
        Vector2 randPointA = new Vector2(
            radiusA * Mathf.Cos(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.x,
            radiusA * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.y
        );

        return randPointA;
    }

    private Vector2 SetRandomBezierPointP3(Vector2 _origin)
    {
        Vector2 randPointB = new Vector2(
            radiusB * Mathf.Cos(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.x,
            radiusB * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) + _origin.y
        );

        return randPointB;
    }
}
