using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{    
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private List<Vector3> _availablePoints;
    [SerializeField] private List<Vector3> _allPoints;
    [SerializeField] private Transform _laserTransform;

    private Vector3 offsetForLinecast = new Vector3(0, -0.5f, 0);

    private int _reward;
    private Vector3 _currentPos;
    private GameObject _currentCoin;

    public void Init(int reward)
    {
        _availablePoints = new List<Vector3>();
        _reward = reward;

        FindPoints();
        SetNextPosition();
        StartCoroutine(CreateNextCoin());

        LevelProgress.OnLevelComplete += Disable;
    }

    public void CreateNextPointHandler()
    {
        StartCoroutine(CreateNextCoin());
    }

    private void FindPoints()
    {
        _allPoints = new List<Vector3>();

        for (int i = 0; i < _levelBuilder.Lines.Count; i++)
        {
            _allPoints.AddRange(_levelBuilder.Lines[i].Points);  //собрали все доступные точки
        }

        foreach (var point in _allPoints)
        {
            if (CommonFindPos(point) != Vector3.zero)
            {
                _availablePoints.Add(point);
            }
        }
    }

    private IEnumerator CreateNextCoin()
    {
        yield return new WaitForSeconds(0.1f);

        _currentCoin = ObjectPool.Instance.GetObject(ObjectType.COIN);

        if(_currentCoin.TryGetComponent(out ICoin coin))
        {            
            coin.Init(_currentPos, _reward);
        }

        SetNextPosition();

        yield break;
    }

    private Vector3 CommonFindPos(Vector3 point)
    {
        return CheckAvailability(point);
    }

    private void SetNextPosition()
    {
        int index = Random.Range(0, _availablePoints.Count);
        _currentPos = _availablePoints[index];
    }

    private Vector3 CheckAvailability(Vector3 pos)
    {
        Vector3 playerPosition = _laserTransform.transform.position;

        bool isAvailability = Physics.Linecast(pos + offsetForLinecast, playerPosition);
                              
             
        if (isAvailability)
        {
            Debug.DrawLine(pos + offsetForLinecast, playerPosition, Color.red, 100);
            return Vector3.zero;
        }
        else
        {
            Debug.DrawLine(pos + offsetForLinecast, playerPosition, Color.green, 100);
            return pos;
        }
    }

    private void Disable()
    {
        ObjectPool.Instance.DestroyObject(_currentCoin);
        LevelProgress.OnLevelComplete -= Disable;
    }
}
