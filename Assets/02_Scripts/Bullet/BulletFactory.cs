using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    [Header("총알 프리팹")]
    private int _bulletCount = 30;
    private int _subBulletCount = 30;

    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    private ObjectPool _mainBulletPool;
    private ObjectPool _subBulletPool;

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
        _mainBulletPool = new ObjectPool();
        _subBulletPool = new ObjectPool();
        _mainBulletPool.InitPool(BulletPrefab, _bulletCount, this.transform);
        _subBulletPool.InitPool(SubBulletPrefab, _subBulletCount, this.transform);
    }

    public GameObject MakeBullet(Vector3 position)
    {
        GameObject bullet = _mainBulletPool.GetObject();
        bullet.SetActive(true);
        bullet.transform.position = position;
        return bullet;
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        GameObject subBullet = _subBulletPool.GetObject();
        subBullet.SetActive(true);
        subBullet.transform.position = position;
        return subBullet;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        EPoolType type = bullet.GetComponent<Bullet>().PoolType;
        if (type == EPoolType.Bullet)
        {
            _mainBulletPool.ReleaseObject(bullet);
        }
        else
        {
            _subBulletPool.ReleaseObject(bullet);
        }
    }
}
