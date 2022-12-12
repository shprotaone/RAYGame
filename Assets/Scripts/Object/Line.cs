using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private SemicircleCreate _semicircleCreator;
    private int _obstacleCount;

    public List<Vector3> Points { get; private set; }
    public List<int> BusyPoints { get; set; }
    public LineType Type { get; private set; }
    public int IndexLine { get; private set; }
    public bool isLineFull { get; private set; }
    public Transform Container { get; private set; }

    public void CreateLine(int indexLine, Transform container)
    {
        Points = new List<Vector3>();
        Points = _semicircleCreator.ArcPoints;
        BusyPoints = new List<int>();

        IndexLine = indexLine;
        Container = container;

        DrawLine();

        LevelBuilder.OnChangeAngle += IncreaseLineLenght;
    }

    public void IncreaseLineLenght(float startAngle,float endAngle)
    {
        SetPoints(startAngle, endAngle, _semicircleCreator.Radius, _semicircleCreator.Segments);
        DrawLine();
    }

    private void DrawLine()
    {
        _lineRenderer.positionCount = Points.Count;
        _lineRenderer.SetPositions(Points.ToArray());
    }

    public void SetPoints(float startAngle ,float endAngle, float radius,int segments)
    {
        _semicircleCreator = new SemicircleCreate(
            startAngle,
            endAngle,
            segments,
            radius
            );

        _semicircleCreator.CreatePoints(transform.position);
    }

    public void SetType(LineType type)
    {
        Type = type;

        if(type == LineType.DYNAMIC)
        {
            _obstacleCount = 1;
        }
        else if(type == LineType.STATIC)
        {
            _obstacleCount = IndexLine - 1;
        }
    }

    public void CheckFillLine()
    {        
        if(_obstacleCount > BusyPoints.Count)
        {
            isLineFull = false;
        }
        else
        {
            isLineFull = true;
        }
    }

    public void DestroyLine()
    {
        Destroy(this.gameObject);
    }
}
