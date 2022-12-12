using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour,IPooledObject,IObstacle
{
    public int PositionIndex { get; set; }
    public ObjectType Type => ObjectType.OBSTACLE;
    public int Penalty { get; private set; }

    public void Init(Line line, int penaltyValue, Vector3 spawnPos)
    {
        transform.SetParent(line.transform);
        transform.position = spawnPos;
        transform.DOLookAt(line.Container.position, 0, AxisConstraint.None, Vector3.forward);
        Penalty = penaltyValue;

        PointSystem.OnLevelComplete += Disable;
    }

    private void Disable()
    {
        ObjectPool.Instance.DestroyObject(gameObject);
    }
}
