using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private PointSystem _pointSystem;
    [SerializeField] private float _beamLenght;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private int _emptyPenalty;
    private float _currentLenght;
    private Vector3 _endPose;

    public void Init()
    {
        _currentLenght = _beamLenght;

        _lineRenderer.useWorldSpace = false;
        _lineRenderer.SetPosition(0,transform.localPosition);
        
    }

    private void FixedUpdate()
    {
        ChangeBeamLenght();
    }

    private void ChangeBeamLenght()
    {
        _endPose = _lineRenderer.transform.TransformVector(_lineRenderer.GetPosition(1));

        RaycastHit hit;

        if(Physics.Raycast(transform.position, _endPose, out hit))
        {
            if (hit.collider != null)
            {
                _currentLenght = hit.distance;               
            }
        }
        else
        {
            _currentLenght = _beamLenght;
        }      

        _lineRenderer.SetPosition(1, Vector3.down * _currentLenght);
    }

    public void Fire()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, _endPose, Color.yellow, 1000f);

        if (Physics.Raycast(transform.position, _endPose, out hit))
        {
            MonoBehaviour behaviour = hit.collider.GetComponent<MonoBehaviour>();

            if (behaviour is IObstacle obstacle)
            {
                _pointSystem.DecreasePoint(obstacle.Penalty);
            }
            else if (behaviour is ICoin coin)
            {
                _pointSystem.IncreasePoint(coin.Reward);
                StartCoroutine(coin.DestroyRoutine());
            }
        }
        else
        {
            _pointSystem.DecreasePoint(_emptyPenalty);
        }
    }
}
