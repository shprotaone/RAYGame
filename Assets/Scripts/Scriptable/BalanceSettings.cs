using UnityEngine;

[CreateAssetMenu]
public class BalanceSettings : ScriptableObject
{
    public int reward;
    public int obstacleFirePenalty;
    public int emptyFirePenalty;
    [Range(0.1f,1)]
    public float minDurationMoveObstacle;
    [Range(0.1f, 1)]
    public float maxDurationMoveObstacle;

    [Header("Только для бесконечного уровня")]
    public int levelStep;
}
