using System;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public static event Action<int> OnIncreaseScore;
    public static event Action<int> OnDecreaseScore;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _beamLenght;

    private int _emptyPenalty;
    private float _currentLenght;
    private Vector3 _endPose;

    public void Init(int penalty)
    {
        _emptyPenalty = penalty;
        _currentLenght = _beamLenght;
        _lineRenderer.enabled = true;

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

        if (Physics.Raycast(transform.position, _endPose, out hit))
        {
            MonoBehaviour behaviour = hit.collider.GetComponent<MonoBehaviour>();

            if (behaviour is IObstacle obstacle)
            {
                OnDecreaseScore?.Invoke(obstacle.Penalty);
                obstacle.Execute();
            }
            else if (behaviour is ICoin coin)
            {
                coin.DestroyHandler();
                OnIncreaseScore?.Invoke(coin.Reward);              
            }
        }
        else
        {
            OnDecreaseScore?.Invoke(_emptyPenalty);
        }
    }
}
