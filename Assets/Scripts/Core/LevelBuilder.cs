using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public static event Action OnLineCreated;
  
    [SerializeField] private BuilderSettings _builderSettings;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _linePrefab;

    private float _startAngle;
    private float _endAngle;
    private int _lineCount;
    private int _pointCountInLine;

    private List<Line> _lines;
    private float _currentRadius;

    public List<Line> Lines => _lines;
    public float StartAngle => _startAngle;
    public float EndAngle => _endAngle;

    public void Init(LevelSettings settings)
    {
        _lines = new List<Line>();

        _startAngle = settings.startAngle;
        _endAngle = settings.endAngle;
        _lineCount = settings.lineCount;
        _pointCountInLine = settings.pointCountInLine;
        _currentRadius = _builderSettings.minRadius;

        CreateLevel();
        LevelProgress.OnLevelComplete += CleanLevel;
        LevelProgress.OnLevelComplete += Disable;
    }

    private void CreateLevel()
    {
        for (int i = 0; i < _lineCount; i++)
        {           
            CreateLine();
            OnLineCreated?.Invoke();
            _currentRadius += _builderSettings.lineStepDistance;
        }
    }

    private void CreateLine()
    {
        GameObject lineGO = ObjectPool.Instance.GetObject(ObjectType.LINE);
        Line line = lineGO.GetComponent<Line>();

        _lines.Add(line);
        line.Init(_startAngle, _endAngle, _currentRadius, _pointCountInLine,_lines.Count,_container);

    }

    private void CleanLevel()
    {
        foreach (var line in _lines)
        {
            line.DestroyLine();
        }

        _lines.Clear();
    }

    public Line GetRandomFreeLine(LineType type)
    {
        foreach (Line line in Lines)
        {
            line.CheckFillLine();

            if (line.ObstacleType == type && !line.IsLineFull)
            {
                return line;
            }
        }

        return null;
    }

    private void Disable()
    {
        LevelProgress.OnLevelComplete -= CleanLevel;
        LevelProgress.OnLevelComplete -= Disable;
    }
}
