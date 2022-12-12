using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    private Sequence _laserSequence;

    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private float _duration;

    public float Duration => _duration;

    private void Init()
    {
        SetRotation(_levelBuilder.StartAngle);
        _laserSequence = DOTween.Sequence();

        LevelBuilder.OnChangeAngle += ChangeLaserRotationLimit;
        LevelBuilder.OnSpeedUp += ChangeSpeedRotation;
        PointSystem.OnLevelComplete += StopLaser;
    }

    private void ChangeSpeedRotation()
    {
        _duration -= -0.1f;
        SetNewSequence(_levelBuilder.StartAngle);
    }

    public void SetRotation(float angle)
    {
        transform.DORotate(new Vector3(0, 0, angle),0);
    }

    public void ChangeLaserRotationLimit(float startAngle,float endAngle)
    {
        SetNewSequence(startAngle);
    }

    private void SetNewSequence(float startAngle)
    {
        SetRotation(startAngle);

        _laserSequence = DOTween.Sequence();

        if(_levelBuilder.StartAngle - _levelBuilder.EndAngle > -179)
        {
            _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle), Duration, RotateMode.Fast)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear));
        }
        else
        {
            _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle + 360), Duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear));
        }
    }

    public void StartMovement()
    {
        Init();

        _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle), Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));          
    }

    public void StopLaser()
    {
        _laserSequence.Pause();
    }

    public void StartLaser()
    {
        _laserSequence.Play();
    }
}
