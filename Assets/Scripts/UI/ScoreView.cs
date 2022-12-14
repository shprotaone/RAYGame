using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{  
    [SerializeField] private float _punchMultiply;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _coinRemaningText;

    private void Start()
    {
        SetColorNotification(0);

        ScoreSystem.OnChangedScore += ChangeScoreText;
        LevelProgress.OnCoinCollect += SetCoinRemainig;
    }

    /// <summary>
    /// Изменение текста
    /// </summary>
    /// <param name="value"></param>
    /// <param name="colorIndex">0-white,1-green,-1-red</param>
    public void ChangeScoreText(int value,int colorIndex)
    {
        SetColorNotification(colorIndex);
        _scoreText.text = value.ToString();
        _scoreText.transform.DOPunchPosition(Vector3.up * _punchMultiply, 0.2f);
    }

    public void SetLevelText(int value)
    {
        _levelText.text = value.ToString();
    }

    public void SetCoinRemainig(int value)
    {
        _coinRemaningText.text = value.ToString();
    }

    private void SetColorNotification(int colorIndex)
    {            
        if(colorIndex == 1)
        {
            _scoreText.color = Color.green;
        }
        else if(colorIndex == -1)
        {
            _scoreText.color = Color.red;
        }
        else
        {
            _scoreText.color = Color.white;
        }

        StartCoroutine(ReturnColor());
    }

    private IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(0.5f);
        _scoreText.color = Color.white;
    }
}
