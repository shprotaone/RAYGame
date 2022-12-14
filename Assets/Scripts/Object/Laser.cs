using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LaserMovement _movement;
    [SerializeField] private Beam _beam;
    [SerializeField] private BeamParticleController _particle;

    private bool _isInitial = false;
    public void Init(int penalty)
    {
        _isInitial = true;
        _movement.Init();
        _beam.Init(penalty);    
    }

    private void Update()
    {
        if (_isInitial)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _movement.StopLaser();
                _beam.Fire();
                _particle.Enable();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _movement.StartLaser();
                _particle.Disable();
            }
        }     
    }
}
