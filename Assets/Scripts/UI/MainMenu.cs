using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _endlessGameButton;
    [SerializeField] private Button _randomGameButton;

    private void Start()
    {
        _playButton.onClick.AddListener(_menuController.StartGame);
        _randomGameButton.onClick.AddListener(_menuController.RandomGame);
        //_endlessGameButton.onClick.AddListener(_menuController.EndlessGame);
        _restartButton.onClick.AddListener(_menuController.RestartGame);
    }
}
