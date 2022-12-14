using DG.Tweening;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{  
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private float _duration;

    private Sequence _laserSequence;

    public float Duration => _duration;

    public void Init()
    {
        _laserSequence = DOTween.Sequence();

        SetRotation(_levelBuilder.StartAngle);      
        StartMovement();

        LevelProgress.OnLevelComplete += DisableBeam;
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void StartMovement()
    {
        _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle), Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));
    }

    public void StopLaser()
    {
        _laserSequence.TogglePause();
    }

    public void StartLaser()
    {
        _laserSequence.Play();
    }

    private void DisableBeam()
    {
        _laserSequence.Rewind();
        _laserSequence.Kill();
        LevelProgress.OnLevelComplete -= DisableBeam;
    }

    #region поворот 360
    //private void SetNewSequence(float startAngle)
    //{
    //    SetRotation(startAngle);

    //    _laserSequence = DOTween.Sequence();

    //    if (_levelBuilder.StartAngle - _levelBuilder.EndAngle > -179)
    //    {
    //        _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle), Duration, RotateMode.Fast)
    //        .SetLoops(-1, LoopType.Yoyo)
    //        .SetEase(Ease.Linear));
    //    }
    //    else
    //    {
    //        _laserSequence.Append(transform.DORotate(new Vector3(0, 0, _levelBuilder.EndAngle + 360), Duration, RotateMode.FastBeyond360)
    //        .SetLoops(-1, LoopType.Yoyo)
    //        .SetEase(Ease.Linear));
    //    }
    //}
    #endregion
}
