using UnityEngine;

public interface ICoin
{
    public int Reward { get; }
    public void Init(Vector3 pos, int reward);
    void DestroyHandler();
}
