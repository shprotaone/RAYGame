using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;

    private ScoreSystem _scoreSystem;

    public void Init(ScoreSystem scoreSystem)
    {
        _scoreSystem = scoreSystem;
        LevelProgress.OnLevelComplete += Show;
    }

    private void Show()
    {
        _resultText.text = _scoreSystem.Score.ToString();
    }
}
