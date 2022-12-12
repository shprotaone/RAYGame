using UnityEngine;

[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    [Range(1, 10)]
    public int lineCount;
    [Range(15,45)]
    public int pointInline;

    public float startAngle = -60;
    public float endAngle = 60;

    public int coinCount;
}
