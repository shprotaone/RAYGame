using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LaserMovement _movement;
    [SerializeField] private Beam _beam;

    public bool IsScoring { get; private set; }
    public bool IsStopped { get; private set; }

    public void Init()
    {
        IsScoring = true;

        _beam.Init();
        _movement.StartMovement();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _movement.StopLaser();
            IsStopped = true;
            _beam.Fire();            
        }

        if (Input.GetMouseButtonUp(0))
        {
            _movement.StartLaser();
            IsStopped = false;
            IsScoring = true;
        }
    }

    public void SetScoring(bool value)
    {
        IsScoring = value;
    }
}
