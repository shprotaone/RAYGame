using UnityEngine;

public class CoinCreator : MonoBehaviour
{
    [SerializeField] private Transform _laserTransform;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _container;

    [SerializeField] private int _reward;
    private Line[] _lines;

    public void Init()
    {        
        SetLines();
        CreateNextCoin();
    }
    
    public void SetLines()
    {
        _lines = _levelBuilder.Lines.ToArray();
    }

    public void CreateNextCoin()
    {
        int indexPos;
        Vector3 pos;

        SetLines();
        Line line = GetLine();   //обновление линии

        indexPos = FindPosition(line);
        pos = line.Points[indexPos];

        if (CheckAvailability(pos))
        {
            GameObject coinGO =ObjectPool.Instance.GetObject(ObjectType.COIN);
            if(coinGO.TryGetComponent(out ICoin coin))
            {
                coin.Init(pos,_reward);
            }           
        }
        else
        {
            CreateNextCoin();
        }    
    }

    private Line GetLine()
    {
        return _lines[UnityEngine.Random.Range(0, _lines.Length-1)];
    }

    private int FindPosition(Line line)
    {    
        int indexPoint = UnityEngine.Random.Range(0, line.Points.Count);

        bool check = CheckFreePosition(line, indexPoint);

        if (check)
        {
            return indexPoint;
        }
        else
        {
            Debug.Log("Данная точка занята");
            return -1;
        }
    }

    private bool CheckFreePosition(Line line, int indexPosition)
    {
        foreach (int index in line.BusyPoints)
        {
            if (index == indexPosition)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckAvailability(Vector3 pos)
    {
        Vector3 playerPosition = _laserTransform.transform.position;

        RaycastHit hit;

        Debug.DrawLine(pos, playerPosition, Color.red, 1000);

        if(Physics.Linecast(pos, playerPosition, out hit))
        {
            if(hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else if(hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("OBSTACLE");
                return false;
            }
        }

        return false;
    }
}
