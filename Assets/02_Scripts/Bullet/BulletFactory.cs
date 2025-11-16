using UnityEngine;
using System.Collections.Generic;

public enum EBulletType
{
    Main,
    Sub,
    BossDirectional,
}

[System.Serializable]
public struct BulletPoolInfo
{
    public EBulletType Type;
    public GameObject Prefab;
    public int InitialSize;
}

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;
    [SerializeField] private BulletPoolInfo[] _bulletPoolInfos;

    private Dictionary<EBulletType, ObjectPool> _bulletPools = new Dictionary<EBulletType, ObjectPool>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        CreatePool();
    }

    private void CreatePool()
    {
        foreach (BulletPoolInfo info in _bulletPoolInfos)
        {
            if (_bulletPools.ContainsKey(info.Type)) continue;
            ObjectPool pool = new ObjectPool();
            pool.InitPool(info.Prefab, info.InitialSize, this.transform);
            _bulletPools.Add(info.Type, pool);
        }
    }

    public GameObject MakeBullet(EBulletType type, Vector3 position)
    {
        GameObject bullet = _bulletPools[type].GetObject();
        IPoolable poolable = bullet.GetComponent<IPoolable>();
        poolable.SetPoolKey((int)type);
        poolable.Init();
        bullet.transform.position = position;
        bullet.SetActive(true);
        return bullet;
    }
    
    public GameObject MakeBullet(EBulletType type, Vector3 position, Vector2 direction)
    {
        GameObject bullet = MakeBullet(type, position);
        bullet.GetComponent<BulletBase>().SetDirection(direction);
        return bullet;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        EBulletType type = (EBulletType)bullet.GetComponent<IPoolable>().PoolKey;
        _bulletPools[type].ReleaseObject(bullet);
    }
}
