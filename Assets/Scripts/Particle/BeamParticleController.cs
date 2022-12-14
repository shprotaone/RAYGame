using UnityEngine;

public class BeamParticleController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ParticleSystem _fireParticle;
    [SerializeField] private ParticleSystem _sparkleParticle;

    public void Enable()
    {
        ChangeParticalLenght();
        _fireParticle.Play();    
    }

    public void Disable()
    {
        _fireParticle.Stop();
    }

    private void ChangeParticalLenght()
    {
        float radius = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(_lineRenderer.positionCount - 1)) / 2;

        var fireShape = _fireParticle.shape;
        var sparkleShape = _sparkleParticle.shape;

        fireShape.position = new Vector3(0, -radius, 0);
        fireShape.radius = radius;

        sparkleShape.position = fireShape.position;
        sparkleShape.radius = fireShape.radius;
    }
}
