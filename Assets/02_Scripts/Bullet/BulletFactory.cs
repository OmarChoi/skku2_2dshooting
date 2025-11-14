using UnityEngine;
using System.Collections.Generic;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    [Header("총알 프리팹")]
    private int _bulletCount = 30;
    private int _subBulletCount = 30;

    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    private Dictionary<EPoolType, ObjectPool> _bulletPools = new Dictionary<EPoolType, ObjectPool>();

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
        ObjectPool subBulletPool = new ObjectPool();
        mainBulletPool.InitPool(BulletPrefab, _bulletCount, this.transform);
        subBulletPool.InitPool(SubBulletPrefab, _subBulletCount, this.transform);
        _bulletPools.Add(EPoolType.Bullet, mainBulletPool);
        _bulletPools.Add(EPoolType.SubBullet, subBulletPool);
    }

    public GameObject MakeBullet(Vector3 position)
    {
        GameObject bullet = _bulletPools[EPoolType.Bullet].GetObject();
        bullet.SetActive(true);
        bullet.GetComponent<IPoolable>().Init();
        bullet.transform.position = position;
        return bullet;
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        GameObject subBullet = _bulletPools[EPoolType.SubBullet].GetObject();
        subBullet.SetActive(true);
        subBullet.GetComponent<IPoolable>().Init();
        subBullet.transform.position = position;
        return subBullet;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        EPoolType type = bullet.GetComponent<Bullet>().PoolType;
        _bulletPools[type].ReleaseObject(bullet);
    }
}
