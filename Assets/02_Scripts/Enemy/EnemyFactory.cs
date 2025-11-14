using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType
{
    Directional,
    Chasing,
    Rush,
}

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    private int _initPoolSize = 10;
    [SerializeField] private GameObject[] _enemyPrefabs;
    private Dictionary<EEnemyType, ObjectPool> _enemyPools = new Dictionary<EEnemyType, ObjectPool>();


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
        foreach (EEnemyType type in System.Enum.GetValues(typeof(EEnemyType)))
        {
            ObjectPool enemyPool = new ObjectPool();
            enemyPool.InitPool(_enemyPrefabs[(int)type], _initPoolSize, this.transform);
            _enemyPools.Add(type, enemyPool);
        }
    }

    public GameObject SpawnEnemy(EEnemyType type, Vector3 position)
    {
        GameObject enemy = _enemyPools[type].GetObject();
        enemy.SetActive(true);
        IPoolable poolable = enemy.GetComponent<IPoolable>();
        poolable.SetPoolKey((int)type);
        poolable.Init();
        enemy.transform.position = position;
        return enemy;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        EEnemyType type = (EEnemyType)enemy.GetComponent<IPoolable>().PoolKey;
        _enemyPools[type].ReleaseObject(enemy);
    }
}
