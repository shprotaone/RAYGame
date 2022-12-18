using System.Collections.Generic;
using UnityEngine;

public class PointChecker
{
    private LevelBuilder _levelBuilder;

    private List<Vector3> _availablePoints;
    private Vector3 _laserPosition;

    public PointChecker(LevelBuilder levelBuilder, Vector3 laserTransform)
    {
        _availablePoints = new List<Vector3>();
        _levelBuilder = levelBuilder;
        _laserPosition = laserTransform;

        FindAvailablePoints();
    }
    public Vector3 GetNextPosition()
    {
        int index = Random.Range(0, _availablePoints.Count);
        return _availablePoints[index];
    }

    private void FindAvailablePoints()
    {
        List<Vector3> allPoints = new List<Vector3>();

        for (int i = 0; i < _levelBuilder.Lines.Count; i++)
        {
            allPoints.AddRange(_levelBuilder.Lines[i].Points);  //собрали все доступные точки
        }

        foreach (var point in allPoints)
        {
            if (CheckHandler(point) != Vector3.zero)
            {
                _availablePoints.Add(point);
            }
        }
    }

    private Vector3 CheckHandler(Vector3 point)
    {
        return CheckAvailability(point);
    }


    private Vector3 CheckAvailability(Vector3 pos)
    {
        Vector3 laserPosition = _laserPosition;

        bool isAvailability = Physics.Linecast(pos, laserPosition);

        if (isAvailability)
        {
            Debug.DrawLine(pos, laserPosition, Color.red, 100);
            return Vector3.zero;
        }
        else
        {
            Debug.DrawLine(pos, laserPosition, Color.green, 100);
            return pos;
        }
    }
}
