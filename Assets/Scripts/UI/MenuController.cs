using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas _inGameCanvas;
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private CanvasGroup _mainMenuCanvasGroup;

    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameInitializator _gameInitializator;

    private void Start()
    {    
        _inGameCanvas.enabled = false;
        LevelProgress.OnLevelComplete += EndGame;
    }

    public void StartGame()
    {
        _gameInitializator.InitCurrentGame();
        FadeCanvas();
    }
    public void EndlessGame()
    {
        _gameInitializator.InitEndless();
        FadeCanvas();
    }

    public void RandomGame()
    {
        _gameInitializator.LevelProgress.ForcedEndGame();
        _gameInitializator.InitRandomLevel();
        FadeCanvas();
    }

    public void RestartGame()
    {
        _gameInitializator.LevelProgress.ForcedEndGame();
        StartCoroutine(_gameInitializator.StartGame());

        AfterStart();
    }

    public void EndGame()
    {
        _endGamePanel.SetActive(true);
        _mainMenu.SetActive(true);
        _mainMenuCanvasGroup.alpha = 1.0f;
        _mainMenuCanvas.enabled = true;
        _inGameCanvas.enabled = false;
    }

    private void AfterStart()
    {
        _mainMenuCanvas.enabled = false;
        _inGameCanvas.enabled = true;
    }

    private void FadeCanvas()
    {
        _mainMenuCanvasGroup.alpha = 0;
        _endGamePanel.gameObject.SetActive(false);
        AfterStart();
    }
}
