using System;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static event Action OnLevelComplete;
    public static event Action OnStartLevel;

    [SerializeField] private PointViewer _pointViewer;
    [SerializeField] private CoinCreator _coinCreator;
    [SerializeField] private LevelProgress _gameContainer;

    private int _score;
    private int _taskCoinCount;
    private int _currentCoinCount;

    public void Init(int coin)
    {
        OnStartLevel?.Invoke();
        _taskCoinCount = coin;
        _currentCoinCount = 0;
        _score = 0;
        _pointViewer.ChangeText(_score);  
    }

    public void IncreasePoint(int point)
    {
        _score += point;
        _pointViewer.ChangeText(_score);
        _coinCreator.CreateNextCoin();

        if (_gameContainer.NextLevelRange < _score)
        {
            _gameContainer.LevelUp();         
        }

        _currentCoinCount++;
        CheckLevelComplete();
    }

    public void DecreasePoint(int point)
    {
        if(_score >= point)
        {
            _score -= point;          
        }
        else
        {
            _score = 0;          
        }

        _pointViewer.ChangeText(_score);
    }

    private void CheckLevelComplete()
    {
        Debug.Log(_currentCoinCount);

        if (_taskCoinCount <= _currentCoinCount)
        {
            OnLevelComplete?.Invoke();
        }
    }
}
