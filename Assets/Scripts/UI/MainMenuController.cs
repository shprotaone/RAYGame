using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Canvas _inGameCanvas;
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private CanvasGroup _mainMenuCanvasGroup;

    [SerializeField] private GameObject _endGameGo;
    [SerializeField] private GameObject _startGameGO;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _endlessGameButton;
    [SerializeField] private Button _randomGameButton;

    [SerializeField] private GameInitializator _gameInitializator;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
        _randomGameButton.onClick.AddListener(RandomGame);
        _endlessGameButton.onClick.AddListener(EndlessGame);

        _restartButton.onClick.AddListener(RestartGame);
        _inGameCanvas.enabled = false;

        PointSystem.OnLevelComplete += EndGame;
    }

    private void StartGame()
    {
        _gameInitializator.InitStartGame();
        FadeCanvas();
    }
    private void EndlessGame()
    {
        _gameInitializator.InitEndless();
        FadeCanvas();
    }

    private void RandomGame()
    {
        _gameInitializator.InitRandomLevel();
        FadeCanvas();
    }

    private void RestartGame()
    {
        _gameInitializator.RestartGame();
    }

    private void EndGame()
    {
        _endGameGo.SetActive(true);
        _startGameGO.SetActive(false);
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
        _mainMenuCanvasGroup.DOFade(0, 1).OnComplete(AfterStart);
        _endGameGo.SetActive(false);
    }
}
