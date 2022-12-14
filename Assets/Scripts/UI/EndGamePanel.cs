using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameInitializator _gameInitializator;
    [SerializeField] private TMP_Text _resultText;
    
    private void Start()
    {
        LevelProgress.OnLevelComplete += Init;
    }

    private void Init()
    {
        _resultText.text = _gameInitializator.ScoreSystem.Score.ToString();
    }
    private void OnDisable()
    {
        //LevelProgress.OnLevelComplete -= Init;
        //LevelProgress.OnStartLevel -= Close;
    }
}
