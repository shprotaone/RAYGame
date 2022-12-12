using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] private Transform _lineContainer;
    [SerializeField] private Line[] _lineCreators;

    [SerializeField] private GameObject _obstacleStaticGO;
    [SerializeField] private GameObject _obstacleMovementGO;
    
    [SerializeField] private int _penalty;
    

    private Line _currentLine;
    public int CurrentIndexPosition { get; private set; }

    public void Init()
    {
        CurrentIndexPosition = 0;
        CreateObstacle();     
        LevelBuilder.OnAddObstacle += CreateObstacle;     
    }

    public void CreateObstacle()
    {
        _lineCreators = _lineContainer.GetComponentsInChildren<Line>();
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
                Debug.LogError("Точка уже занята");
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

            dynamicObstacleGO.TryGetComponent(out IObstacle dynamicObstacle);
            dynamicObstacle.Init(_currentLine, _penalty, pos);

        }
    }

    private Line GetLine(LineType type)
    {
        foreach (Line line in _lineCreators)
        {
            line.CheckFillLine();

            if(line.Type == type && !line.isLineFull)
            {
                return line;
            }
        }

        Debug.LogWarning("Линия не найдена");
        return null;
    }

    private Vector3 FindPosition()
    {
        CurrentIndexPosition = Random.Range(0, _currentLine.Points.Count);

        Vector3 position = _currentLine.Points[CurrentIndexPosition];

        for (int i = 0; i < _currentLine.transform.childCount; i++)
        {
            if (_currentLine.transform.GetChild(i).position == position)
            {
                FindPosition();                
            }
        }

        return _currentLine.Points[CurrentIndexPosition];
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
}
