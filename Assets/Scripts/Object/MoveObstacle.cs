using DG.Tweening;
using UnityEngine;

public class MoveObstacle : MonoBehaviour, IObstacle, IPooledObject
{
    [SerializeField] private ParticleController _particleController;
    
    private Line _line;
    private Direction _direction;
    
    private int _index;
    private float _durationTween;

    public int Penalty { get; private set; }
    public ObjectType Type => ObjectType.DYNAMICOBSTACLE;

    public void Init(Line line, int penaltyValue,Vector3 spawnPos)
    {
        Penalty = penaltyValue;
        _line = line;
        _index = Random.Range(0, 1);

        transform.position = spawnPos;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        FindMovementDirection();
        Move();

        LevelProgress.OnLevelComplete += Disable;
    }
    
    public void Execute()
    {
        _particleController.Play();
    }

    public void SetDuration(float min, float max)
    {
        _durationTween = Random.Range(min, max);
    }

    private void Move()
    {
        transform.DODynamicLookAt(_line.transform.position,_durationTween,AxisConstraint.None,Vector3.forward);
        transform.DOMoveX(_line.Points[_index].x, _durationTween).OnComplete(NextPoint).SetEase(Ease.Linear);        
        transform.DOMoveY(_line.Points[_index].y, _durationTween).SetEase(Ease.Linear);       
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

        ChangeDirection();
        Move();
    }

    private void ChangeDirection()
    {
        if (_index == 0)
        {
            _direction = Direction.LEFT;
        }
        else if (_index == _line.Points.Count - 1)
        {
            _direction = Direction.RIGHT;
        }
    }

    private void FindMovementDirection()
    {
        int dir = Random.Range(0, 1);
        _direction = (Direction)dir;
    }

    private void Disable()
    {
        DOTween.Rewind(transform);
        DOTween.Kill(transform);
        
        ObjectPool.Instance.DestroyObject(this.gameObject);
        LevelProgress.OnLevelComplete -= Disable;
    } 
}
    

