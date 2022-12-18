using System.Collections;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [Header("Инициализация систем")]
    [SerializeField] private ObstacleCreator _objectCreator;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private CoinCreator _coinCreator;   
    [SerializeField] private Laser _laser;
    [SerializeField] private MenuController _menuController;
     
    private LevelRandomizer _levelRandomizer;
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
        _menuController.Init();
        
        _levelBuilder.Init(_currentLevelSettings);
        _objectCreator.Init(_balanceSettings.obstacleFirePenalty,
                            _balanceSettings.minDurationMoveObstacle,
                            _balanceSettings.maxDurationMoveObstacle,
                            _levelBuilder);            

        yield return new WaitForSeconds(0.2f);  //задержка для генерации препятствий

        _coinCreator.Init(_balanceSettings.reward,_levelBuilder,_laser.transform.position);
        _laser.Init(_balanceSettings.emptyFirePenalty,_levelBuilder);

        yield break;
    }

    public void InitEndless()
    {
        _currentLevelSettings = _endlessLevelSettings;
        StartCoroutine(StartGame());
    }

    public void InitRandomLevel()
    {
        _levelRandomizer = new LevelRandomizer();
        _levelRandomizer.GenerateRandomLevel();

        _randomLevel.coinCount = _levelRandomizer.CoinCount;
        _randomLevel.lineCount = _levelRandomizer.LineCount;
        _randomLevel.pointCountInLine = _levelRandomizer.PointCountInLine;

        _currentLevelSettings = _randomLevel;
        StartCoroutine(StartGame());
    }
}
