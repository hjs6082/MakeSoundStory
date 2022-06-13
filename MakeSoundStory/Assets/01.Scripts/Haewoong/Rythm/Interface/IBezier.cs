using UnityEngine;

public interface IBezier
{
    void SetObjects(Vector2 _vec);
    Vector2 MoveBezier();
    float FourPointBezier(float a, float b, float c, float d);
    Vector2 SetRandomBezierPointP2(Vector2 _origin);
    Vector2 SetRandomBezierPointP3(Vector2 _origin);
}
