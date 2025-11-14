using UnityEngine;

public interface IPoolable
{
    void Init();
    void Release();
    void SetPoolKey(int key);
    int PoolKey { get; }
}
