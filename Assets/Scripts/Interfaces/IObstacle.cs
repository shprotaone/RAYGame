using UnityEngine;

public interface IObstacle
{
    public int Penalty { get;}
    void Init(Line line,int penaltyValue,Vector3 spawnPos);
}
