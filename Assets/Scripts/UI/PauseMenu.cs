using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _randomLevelButton;

    private void Start()
    {
        LevelProgress.OnStartLevel += DisablePausePanel;

        _resetButton.onClick.AddListener(_menuController.RestartGame);
        _randomLevelButton.onClick.AddListener(_menuController.RandomGame);
        _backButton.onClick.AddListener(Back);
        _pauseButton.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);        
    }

    private void Back()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);       
    }

    private void DisablePausePanel()
    {
        _pausePanel.SetActive(false);
    }
}
