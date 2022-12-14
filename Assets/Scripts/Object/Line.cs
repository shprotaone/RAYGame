using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour,IPooledObject
{
    [SerializeField] private LineRenderer _lineRenderer;

    private SemicircleCreate _semicircleCreator;
    private int _obstacleCount;

    public List<Vector3> Points { get; private set; }
    public List<int> BusyPoints { get; private set; }
    public LineType ObstacleType { get; private set; }
    public int IndexLine { get; private set; }
    public bool IsLineFull { get; private set; }
    public Transform Container { get; private set; }

    public ObjectType Type => ObjectType.LINE;

    public void Init(float startAngle, float endAngle, float radius, int segments, int indexLine, Transform container)
    {
        IndexLine = indexLine;
        Container = container;

        transform.SetParent(Container);
        SetPoints(startAngle, endAngle, radius, segments);
        CreateLine(indexLine);
        SetLineType();
    }

    private void SetPoints(float startAngle, float endAngle, float radius, int segments)
    {
        _semicircleCreator = new SemicircleCreate(
            startAngle,
            endAngle,
            segments,
            radius
            );

        _semicircleCreator.CreatePoints(Vector3.zero);
    }

    private void CreateLine(int indexLine)
    {
        Points = new List<Vector3>();
        Points = _semicircleCreator.ArcPoints;
        BusyPoints = new List<int>();

        IndexLine = indexLine;
        DrawLine();

    }

    private void SetLineType()
    {
        if (IndexLine == 0)
        {
            ObstacleType = LineType.EMPTY;
        }
        else if (IndexLine % 3 == 0)
        {
            ObstacleType = LineType.DYNAMIC;
            _obstacleCount = 1;
        }
        else
        {
            ObstacleType = LineType.STATIC;
            _obstacleCount = IndexLine - 1;
        }
    }

    private void DrawLine()
    {
        _lineRenderer.positionCount = Points.Count;
        _lineRenderer.SetPositions(Points.ToArray());
    }

    public void CheckFillLine()
    {        
        if(_obstacleCount > BusyPoints.Count)
        {
            IsLineFull = false;
        }
        else
        {
            IsLineFull = true;
        }
    }

    public void DestroyLine()
    {
        ObjectPool.Instance.DestroyObject(this.gameObject);
    }

    #region поворот 360
    //public void IncreaseLineLenght(float startAngle, float endAngle)
    //{
    //    SetPoints(startAngle, endAngle, _semicircleCreator.Radius, _semicircleCreator.Segments);
    //    DrawLine();
    //}
    #endregion
}
