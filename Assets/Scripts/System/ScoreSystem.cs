using System;

public class ScoreSystem
{
    public static event Action<int,int> OnChangedScore;

    private CoinCreator _coinCreator;
    private int _score;

    public int Score => _score;
    public ScoreSystem(CoinCreator coinCreator)
    {
        _coinCreator = coinCreator;

        Beam.OnIncreaseScore += IncreaseScore;
        Beam.OnDecreaseScore += DecreaseScore;
        LevelProgress.OnLevelComplete += Disable;
    }

    public void Init()
    {       
        _score = 0;
        OnChangedScore?.Invoke(_score,0);
    }

    public void IncreaseScore(int point)
    {
        _score += point;
        _coinCreator.CreateNextPointHandler();

        OnChangedScore?.Invoke(_score,1);
    }

    public void DecreaseScore(int point)
    {
        if(_score >= point)
        {
            _score -= point;          
        }
        else
        {
            _score = 0;          
        }

        OnChangedScore?.Invoke(_score,-1);
    }

    private void Disable()
    {
        Beam.OnIncreaseScore -= IncreaseScore;
        Beam.OnDecreaseScore -= DecreaseScore;
        LevelProgress.OnLevelComplete -= Disable;
    }
}
