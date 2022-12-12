using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private ObstacleCreator _objectCreator;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private CoinCreator _coinCreator;
    [SerializeField] private PointSystem _pointSystem;
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private LevelSettings _endlessLevelSettings;

    [SerializeField] private Laser _laser;

    [SerializeField] private LevelSettings _randomLevel;
    [SerializeField] private LevelRandomizer _levelRandomizer;

    private LevelSettings _currentLevelSettings;
    public void InitStartGame()
    {
        _currentLevelSettings = _levelSettings;
        RestartGame();
    }

    public void RestartGame()
    {
        _pointSystem.Init(_currentLevelSettings.coinCount);
        _levelBuilder.Init(_currentLevelSettings);
        _objectCreator.Init();
        _levelProgress.Init();
        _laser.Init();

        _coinCreator.Init();
    }

    public void InitEndless()
    {
        _currentLevelSettings = _endlessLevelSettings;
        RestartGame();
    }

    public void InitRandomLevel()
    {
        _levelRandomizer.GenerateRandomLevel();

        _currentLevelSettings = _randomLevel;
        RestartGame();
    }
}
