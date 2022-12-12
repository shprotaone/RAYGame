using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemicircleCreate
{
    private float _startAngle, _endAngle;
    private int _segments;
    private float _radius;

    private List<Vector3> _arcPoints = new List<Vector3>();
    public List<Vector3> ArcPoints => _arcPoints;
    public int Segments => _segments;
    public float Radius => _radius;


    public SemicircleCreate(float startAngle, float endAngle, int segments, float radius)
    {
        this._startAngle = startAngle;
        this._endAngle = endAngle;
        this._segments = segments;
        this._radius = radius;
    }

    public void CreatePoints(Vector3 center)
    {
        _arcPoints.Clear();

        float angle = _startAngle;
        float arcLength = _endAngle - _startAngle;

        for (int i = 0; i <= _segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

            _arcPoints.Add(new Vector3(-x, -y) + center);

            angle += (arcLength / _segments);
        }


        //for (int j = 0; j < _segments; j++)
        //{
        //    Debug.DrawLine(_arcPoints[j], _arcPoints[j + 1], Color.red,1);
        //}
    }
}
