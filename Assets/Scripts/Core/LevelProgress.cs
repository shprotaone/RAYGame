using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static event Action OnChangedLevel;

    public readonly int levelStep = 500;

    [SerializeField] private PointViewer _pointViewer;
    [SerializeField] private int _level;
    private int _nextLevelRangePoint;

    public int CurrentLevel => _level;
    public int NextLevelRange => _nextLevelRangePoint;

    public void Init() 
    {
        _nextLevelRangePoint = levelStep;
        _level = 1;
        _pointViewer.SetLevelText(_level);
    }

    public void LevelUp()
    {
        _level++;
        _nextLevelRangePoint += levelStep;
        _pointViewer.SetLevelText(CurrentLevel);
        OnChangedLevel?.Invoke();
    }
}
