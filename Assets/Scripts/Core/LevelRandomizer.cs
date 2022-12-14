using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{
    [SerializeField] private LevelSettings _levelSettings;

    public LevelSettings GenerateRandomLevel()
    {
        _levelSettings.lineCount = Random.Range(3, 15);
        _levelSettings.coinCount = Random.Range(15, 30);
        _levelSettings.pointInline = Random.Range(10, 30);

        return _levelSettings;
    }
}
