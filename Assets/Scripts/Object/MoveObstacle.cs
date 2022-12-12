using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour,IObstacle
{
    private float _duration = 0.2f;
    private Line _line;

    private Direction _direction;
    private int _index;

    public int Penalty { get; private set; }

    public void Init(Line line, int penaltyValue,Vector3 spawnPos)
    {
        Penalty = penaltyValue;
        _line = line;
        _index = Random.Range(0, 1);

        transform.SetParent(line.transform);
        transform.DOLookAt(line.Container.position, 0, AxisConstraint.None, Vector3.forward);

        FindMovementDirection();
        Move();

        PointSystem.OnLevelComplete += Disable;
    }

    private void Move()
    {
        transform.DODynamicLookAt(_line.transform.position, _duration, AxisConstraint.Z, Vector3.forward);
        transform.DOMoveX(_line.Points[_index].x, _duration).OnComplete(NextPoint).SetEase(Ease.Linear);        
        transform.DOMoveY(_line.Points[_index].y, _duration).SetEase(Ease.Linear);       
    }

    private void NextPoint()
    {
        if (_direction == Direction.LEFT)
        {
            _index += 1;
        }
        else
        {
            _index -= 1;
        }

        if (_index == 0)
        {
            _direction = Direction.LEFT;
        }
        else if (_index == _line.Points.Count - 1)
        {
            _direction = Direction.RIGHT;
        }

        Move();
    }
    private void FindMovementDirection()
    {
        int dir = Random.Range(0, 1);
        _direction = (Direction)dir;
    }

    private void Disable()
    {
        DOTween.PauseAll();
    }
}
    

