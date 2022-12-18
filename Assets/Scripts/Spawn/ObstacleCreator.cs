using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{   
    private LevelBuilder _levelBuilder;

    private int _penalty;
    private float _minDurationMove;
    private float _maxDurationMove;

    public int CurrentIndexPosition { get; private set; }

    public void Init(int penalty, float minDurationMove,float maxDurationMove,LevelBuilder levelBuilder)
    {
        CurrentIndexPosition = 0;
        _minDurationMove = minDurationMove;
        _maxDurationMove = maxDurationMove;
        _penalty = penalty;
        _levelBuilder = levelBuilder;

        CreateObstacle(_levelBuilder.Lines);

        LevelProgress.OnLevelComplete += Disable;
    }

    public void CreateObstacle(List<Line> lines)
    {
        Line line;
        for (int i = 0; i < lines.Count; i++)
        {
            line = lines[i];
            if(line.ObstacleType == LineType.STATIC)
            {
                CreateStaticObstacle(line);
            }
            else if(line.ObstacleType == LineType.DYNAMIC)
            {
                CreateMovementObstacle(line);
            }
        }
    }

    private void CreateStaticObstacle(Line line)
    {
        if (line != null)
        {
            Vector3 pos = FindPosition(line);
            bool pointIsFree = CheckFreePosition(line, CurrentIndexPosition);

            if (pointIsFree)
            {
                GameObject obstacleGO = ObjectPool.Instance.GetObject(ObjectType.OBSTACLE);

                obstacleGO.TryGetComponent(out IObstacle obstacle);
                obstacle.Init(line, _penalty, pos);

                line.BusyPoints.Add(CurrentIndexPosition);
            }
            else
            {
                CreateStaticObstacle(line);                
            }
        }
    }

    private void CreateMovementObstacle(Line line)
    {
        
        if (line != null)
        {
            Vector3 pos = FindPosition(line);
            GameObject dynamicObstacleGO = ObjectPool.Instance.GetObject(ObjectType.DYNAMICOBSTACLE);

            line.BusyPoints.Add(CurrentIndexPosition);

            if(dynamicObstacleGO.TryGetComponent(out IObstacle dynamicObstacle))
            {
                dynamicObstacle.Init(line, _penalty, pos);
                
                if (dynamicObstacle is MoveObstacle obstacle)
                {
                    obstacle.SetDuration(_minDurationMove, _maxDurationMove);
                }        
            }            
        }
    }  

    private Vector3 FindPosition(Line line)
    {
        CurrentIndexPosition = Random.Range(1, line.Points.Count - 1);

        Vector3 position = line.Points[CurrentIndexPosition];

        return position;
    }

    private bool CheckFreePosition(Line line, int indexPosition)
    {
        foreach (int index in line.BusyPoints)
        {
            if(index == indexPosition)
            {
                return false;
            }
        }

        return true;
    }

    private void Disable()
    {
        LevelProgress.OnLevelComplete -= Disable; 
    }
}
