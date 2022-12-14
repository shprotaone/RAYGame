using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Play()
    {
        _particleSystem.Play();
    }
}
