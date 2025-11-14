using UnityEngine;
public enum EPoolType
{
    Bullet,
    SubBullet,
    Enemy,
    Effect
}

public interface IPoolable
{
    EPoolType PoolType { get; }
    void Init();
    void Release();
}
