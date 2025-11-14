using UnityEngine;
using System.Collections.Generic;

public enum EBulletType
{
    Main,
    Sub
}

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    [Header("총알 프리팹")]
    private int _bulletCount = 30;
    private int _subBulletCount = 30;

    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

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
        ObjectPool mainBulletPool = new ObjectPool();
        mainBulletPool.InitPool(BulletPrefab, _bulletCount, this.transform);
        _bulletPools.Add(EBulletType.Main, mainBulletPool);

        ObjectPool subBulletPool = new ObjectPool();
        subBulletPool.InitPool(SubBulletPrefab, _subBulletCount, this.transform);
        _bulletPools.Add(EBulletType.Sub, subBulletPool);
    }

    public GameObject MakeBullet(EBulletType type, Vector3 position)
    {
        GameObject bullet = _bulletPools[type].GetObject();
        bullet.SetActive(true);
        IPoolable poolable = bullet.GetComponent<IPoolable>();
        poolable.SetPoolKey((int)type);
        poolable.Init();
        bullet.transform.position = position;
        return bullet;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        EBulletType type = (EBulletType)bullet.GetComponent<IPoolable>().PoolKey;
        _bulletPools[type].ReleaseObject(bullet);
    }
}
