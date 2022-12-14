using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private GameObject _obstacleStaticGO;
    [SerializeField] private GameObject _obstacleMovementGO;
    
    private int _penalty;
    private float _minDurationMove;
    private float _maxDurationMove;
    
    private Line _currentLine;
    public int CurrentIndexPosition { get; private set; }

    public void Init(int penalty, float minDurationMove,float maxDurationMove)
    {
        CurrentIndexPosition = 0;
        _minDurationMove = minDurationMove;
        _maxDurationMove = maxDurationMove;
        _penalty = penalty;      
        LevelProgress.OnLevelComplete += Disable;
        LevelBuilder.OnLineCreated += CreateObstacle;
    }

    public void CreateObstacle()
    {
        CreateMovementObstacle();
        CreateStaticObstacle();
    }

    private void CreateStaticObstacle()
    {
        _currentLine = GetLine(LineType.STATIC);

        if (_currentLine != null)
        {
            Vector3 pos = FindPosition();
            bool pointIsFree = CheckFreePosition(_currentLine, CurrentIndexPosition);

            if (pointIsFree)
            {
                GameObject obstacleGO = ObjectPool.Instance.GetObject(ObjectType.OBSTACLE);

                obstacleGO.TryGetComponent(out IObstacle obstacle);
                obstacle.Init(_currentLine, _penalty,pos);

                _currentLine.BusyPoints.Add(CurrentIndexPosition);
            }
            else
            {
                CreateStaticObstacle();                
            }
        }
    }

    private void CreateMovementObstacle()
    {
        _currentLine = GetLine(LineType.DYNAMIC);     

        if (_currentLine != null)
        {
            Vector3 pos = FindPosition();
            GameObject dynamicObstacleGO = ObjectPool.Instance.GetObject(ObjectType.DYNAMICOBSTACLE);

            _currentLine.BusyPoints.Add(CurrentIndexPosition);

            if(dynamicObstacleGO.TryGetComponent(out IObstacle dynamicObstacle))
            {
                dynamicObstacle.Init(_currentLine, _penalty, pos);
                
                if (dynamicObstacle is MoveObstacle obstacle)
                {
                    obstacle.SetDuration(_minDurationMove, _maxDurationMove);
                }        
            }            
        }
    }

    private Line GetLine(LineType type)
    {
        foreach (Line line in _levelBuilder.Lines)
        {
            line.CheckFillLine();

            if(line.ObstacleType == type && !line.IsLineFull)
            {
                return line;
            }
        }

        return null;
    }

    private Vector3 FindPosition()
    {
        CurrentIndexPosition = Random.Range(1, _currentLine.Points.Count - 1);

        Vector3 position = _currentLine.Points[CurrentIndexPosition];

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
        LevelBuilder.OnLineCreated -= CreateObstacle;
    }
}
