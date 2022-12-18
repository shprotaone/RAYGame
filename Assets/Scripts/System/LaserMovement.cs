using DG.Tweening;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{      
    [SerializeField] private float _duration;

    private Sequence _laserSequence;

    private LevelBuilder _levelBuilder;
    public float Duration => _duration;

    public void Init(LevelBuilder levelBuilder)
    {
        _laserSequence = DOTween.Sequence();
        _levelBuilder = levelBuilder;
        SetStartRotation(_levelBuilder.StartAngle);      
        StartMovement();

        LevelProgress.OnLevelComplete += DisableBeam;
    }

    public void SetStartRotation(float angle)
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
