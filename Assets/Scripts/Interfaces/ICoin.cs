using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoin
{
    public int Reward { get; }
    public void Init(Vector3 pos, int reward);
    IEnumerator DestroyRoutine();
}
