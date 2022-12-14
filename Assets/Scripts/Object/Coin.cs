using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour, ICoin,IPooledObject
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _duration = 1;

    private Vector3 _rotateDirection = new Vector3(0, 20, 0);

    public ObjectType Type => ObjectType.COIN;
    public int Reward { get; private set; }

    public void Init(Vector3 pos,int reward)
    {
        Reward = reward;
        transform.position = pos;

        _transform.gameObject.SetActive(true);
        _boxCollider.enabled = true;  
        transform.DORotate(_rotateDirection, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);    
        
        LevelProgress.OnLevelComplete += Disable;
    }

    public void DestroyHandler()
    {
        StartCoroutine(DestroyRoutine());
    }

    public IEnumerator DestroyRoutine()
    {
        _particleSystem.Play();
        _transform.gameObject.SetActive(false);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_particleSystem.main.duration);

        Disable();

        yield break;
    }

    private void Disable()
    {
        DOTween.Kill(transform);
        ObjectPool.Instance.DestroyObject(gameObject);
        LevelProgress.OnLevelComplete -= Disable;
    }
}
