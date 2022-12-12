using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public static event Action OnLineCreate;
    public static event Action OnAddObstacle;
    public static event Action OnSpeedUp;
    public static event Action<float,float> OnChangeAngle;

    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private LevelProgress _gameContainer;
    [SerializeField] private float _minRadius = 2;
    [SerializeField] private float _lineStepDistance = 0.5f;

    private float _startAngle;
    private float _endAngle;
    private int _lineCount;
    private int _pointInLine;

    private List<Line> _lines = new List<Line>();
    private float _currentRadius;

    public List<Line> Lines => _lines;
    public float StartAngle => _startAngle;
    public float EndAngle => _endAngle;

    public void Init(LevelSettings settings)
    {
        _startAngle = settings.startAngle;
        _endAngle = settings.endAngle;
        _lineCount = settings.lineCount;
        _pointInLine = settings.pointInline;
        _currentRadius = _minRadius;


        CreateLevel();
        LevelProgress.OnChangedLevel += LevelUp;
        PointSystem.OnStartLevel += CleanLevel;
    }

    private void CreateLevel()
    {
        for (int i = 0; i < _lineCount; i++)
        {           
            CreateLine();
            _currentRadius += _lineStepDistance;
        }
    }

    public void LevelUp()
    {
        int level = _gameContainer.CurrentLevel;
        
        if(_gameContainer.CurrentLevel % 6 == 0)
        {

        }
        else if(_gameContainer.CurrentLevel % 5 == 0)
        {

        }
        else if (_gameContainer.CurrentLevel % 4 == 0)
        {
            Debug.Log("SpeedUp");
            OnSpeedUp?.Invoke();
        }
        else if (_gameContainer.CurrentLevel % 3 == 0)
        {
            if (_container.childCount < 8)
            {
                CreateLine();
                _currentRadius += _lineStepDistance;
            }
        }
        
        if (_gameContainer.CurrentLevel % 2 == 0)
        {
            OnAddObstacle?.Invoke();
        }

            //else if(-(_startAngle) + _endAngle < 360)
            //{
            //    _startAngle -= 11;
            //    _endAngle += 11;
            //    OnChangeAngle?.Invoke(_startAngle, _endAngle);
            //    ChangeLenghtLine();

            //    Debug.Log("SpeedUP");
            //}
    }

    private void CreateLine()
    {
        GameObject lineGO = Instantiate(_linePrefab, _container.transform.position, Quaternion.identity, _container);
        Line line = lineGO.GetComponent<Line>();

        line.SetPoints(_startAngle,_endAngle, _currentRadius, _pointInLine);

        _lines.Add(line);
        line.CreateLine(_lines.Count, _container);
        SetLineType(line, _lines.Count);

        OnLineCreate?.Invoke();
    }

    private void SetLineType(Line line, int numberOfLine)
    {
        if (numberOfLine == 0)
        {
            line.SetType(LineType.EMPTY);
        }
        else if (numberOfLine % 3 == 0)
        {
            line.SetType(LineType.DYNAMIC);
        }
        else if(numberOfLine % 2 == 0)
        {
            line.SetType(LineType.STATIC);         
        }
    }

    /// <summary>
    /// Изменение длины всех линий
    /// </summary>
    private void ChangeLenghtAllLines()
    {
        foreach (var line in _lines)
        {
            line.IncreaseLineLenght(_startAngle,_endAngle);
        }
    }

    private void CleanLevel()
    {
        foreach (var line in _lines)
        {
            line.DestroyLine();
        }

        _lines.Clear();

    }
}
