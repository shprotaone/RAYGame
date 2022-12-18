using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _endlessGameButton;
    [SerializeField] private Button _randomGameButton;

    public void Init(MenuController menuController)
    {
        _playButton.onClick.AddListener(menuController.StartGame);
        _randomGameButton.onClick.AddListener(menuController.RandomGame);
        //_endlessGameButton.onClick.AddListener(_menuController.EndlessGame);
        _restartButton.onClick.AddListener(menuController.RestartGame);
    }
}
