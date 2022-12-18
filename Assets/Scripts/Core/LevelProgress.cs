using System;

public class LevelProgress
{
    public static event Action OnStartLevel;
    public static event Action OnLevelComplete; 
    public static event Action<int> OnCoinCollect;

    private int _taskCoinCount;
    private int _currentCoinCount;

    public LevelProgress(int taskCoinCount)
    {
        _currentCoinCount = 0;
        _taskCoinCount = taskCoinCount;
        Beam.OnIncreaseScore += CollectedCoin;
        OnLevelComplete += Disable;
        OnCoinCollect?.Invoke(_taskCoinCount - _currentCoinCount);
        OnStartLevel?.Invoke();
    }

    private void CollectedCoin(int value)
    {
        _currentCoinCount++;
        CheckLevelComplete();
        OnCoinCollect?.Invoke(_taskCoinCount - _currentCoinCount);
    }

    private void CheckLevelComplete()
    {
        if (_taskCoinCount <= _currentCoinCount)
        {
            OnLevelComplete?.Invoke();
        }
    }

    public void ForcedEndGame()
    {
        OnLevelComplete?.Invoke();
    }

    private void Disable()
    {
        Beam.OnIncreaseScore -= CollectedCoin;
        OnLevelComplete -= Disable;
    }

    #region под бесконечный уровень 

    //public void Init() 
    //{
    //    //_nextLevelRangePoint = levelStep;
    //    _level = 1;
    //    _pointViewer.SetLevelText(_level);
    //}

    //public void LevelUp()
    //{
    //    _level++;
    //    _nextLevelRangePoint += levelStep;
    //    _pointViewer.SetLevelText(CurrentLevel);
    //    OnChangedLevel?.Invoke(_level);
    //}

    //public void LevelUp(int level)
    //{
    //    level = _levelPr.CurrentLevel;        

    //    if (_levelPr.CurrentLevel % 4 == 0)
    //    {
    //        Debug.Log("SpeedUp");
    //        OnSpeedUp?.Invoke();
    //    }
    //    else if (_levelPr.CurrentLevel % 3 == 0)
    //    {
    //        if (_container.childCount < _builderSettings.maximumLine)
    //        {
    //            CreateLine();
    //            _currentRadius += _builderSettings.lineStepDistance;
    //        }
    //    }

    //    if (_levelPr.CurrentLevel % 2 == 0)
    //    {

    //    }

    //    //else if(-(_startAngle) + _endAngle < 360)
    //    //{
    //    //    _startAngle -= 11;
    //    //    _endAngle += 11;
    //    //    OnChangeAngle?.Invoke(_startAngle, _endAngle);
    //    //    ChangeLenghtLine();

    //    //    Debug.Log("SpeedUP");
    //    //}
    //}
    #endregion
}
