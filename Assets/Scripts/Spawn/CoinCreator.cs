using UnityEngine;

public class CoinCreator : MonoBehaviour
{       
    private PointChecker _pointChecker;

    private int _reward;
    private Vector3 _currentPos;
    private GameObject _currentCoin;

    public void Init(int reward,LevelBuilder levelBuilder,Vector3 laserPos)
    {        
        _reward = reward;
        _pointChecker = new PointChecker(levelBuilder,laserPos);

        _currentPos = _pointChecker.GetNextPosition();
        CreateNextCoin();

        LevelProgress.OnLevelComplete += Disable;
    }  

    public void CreateNextCoin()
    {
        _currentCoin = ObjectPool.Instance.GetObject(ObjectType.COIN);

        if(_currentCoin.TryGetComponent(out ICoin coin))
        {            
            coin.Init(_currentPos, _reward);
        }

        _currentPos = _pointChecker.GetNextPosition();
    }

    private void Disable()
    {
        ObjectPool.Instance.DestroyObject(_currentCoin);
        LevelProgress.OnLevelComplete -= Disable;
    }
}
