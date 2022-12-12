using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour, ICoin,IPooledObject
{
    private const int pointReward = 200;

    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _model;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _duration = 1;
    [SerializeField] private ParticleSystem _particleSystem;

    public ObjectType Type => ObjectType.COIN;
    public int Reward { get; private set; }

    public void Init(Vector3 pos,int reward)
    {
        Reward = reward;
        transform.DORotate(_direction, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        transform.position = pos;
        PointSystem.OnLevelComplete += Disable;
    }

    public IEnumerator DestroyRoutine()
    {
        _particleSystem.Play();
        _model.gameObject.SetActive(false);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_particleSystem.main.duration);

        Disable();
        _model.gameObject.SetActive(true);
        _boxCollider.enabled = true;
    }

    private void Disable()
    {
        ObjectPool.Instance.DestroyObject(gameObject);
    }
}
