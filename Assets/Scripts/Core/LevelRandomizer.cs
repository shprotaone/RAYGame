using UnityEngine;

public class LevelRandomizer
{
    public int LineCount { get; private set; }
    public int CoinCount { get; private set; }
    public int PointCountInLine { get; private set; }

    public void GenerateRandomLevel()
    {
        LineCount = Random.Range(3, 15);
        CoinCount = Random.Range(15, 30);
        PointCountInLine = Random.Range(10, 30);
    }
}
