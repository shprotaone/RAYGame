using System.Collections;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [Header("Инициализация систем")]
    [SerializeField] private ObstacleCreator _objectCreator;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private CoinCreator _coinCreator;   
    [SerializeField] private Laser _laser;    
    [SerializeField] private LevelRandomizer _levelRandomizer;

    private ScoreSystem _scoreSystem;
    private LevelProgress _levelProgress;

    [Space]
    [Header("Глобальные настройки")]

    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private LevelSettings _endlessLevelSettings;
    [SerializeField] private LevelSettings _randomLevel;

    [SerializeField] private BalanceSettings _balanceSettings;
    private LevelSettings _currentLevelSettings;

    public ScoreSystem ScoreSystem => _scoreSystem;
    public LevelProgress LevelProgress => _levelProgress;
    public void InitCurrentGame()
    {      
        _currentLevelSettings = _levelSettings;
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        Time.timeScale = 1;
        _scoreSystem = new ScoreSystem(_coinCreator);
        _levelProgress = new LevelProgress(_currentLevelSettings.coinCount);

        _objectCreator.Init(_balanceSettings.obstacleFirePenalty,
                            _balanceSettings.minDurationMoveObstacle,
                            _balanceSettings.maxDurationMoveObstacle);   
        
        _levelBuilder.Init(_currentLevelSettings);

        yield return new WaitForSeconds(0.2f);  //задержка для генерации препятствий

        _coinCreator.Init(_balanceSettings.reward);
        _scoreSystem.Init();               
        _laser.Init(_balanceSettings.emptyFirePenalty);

        yield break;
    }

    public void InitEndless()
    {
        _currentLevelSettings = _endlessLevelSettings;
        StartCoroutine(StartGame());
    }

    public void InitRandomLevel()
    {
        _levelRandomizer.GenerateRandomLevel();

        _currentLevelSettings = _randomLevel;
        StartCoroutine(StartGame());
    }
}
