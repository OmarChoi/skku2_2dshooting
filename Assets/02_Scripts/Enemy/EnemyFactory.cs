using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType
{
    Directional,
    Chasing,
    Rush,
}

[System.Serializable]
public struct EnemyPrefabPair
{
    public EEnemyType Type;
    public GameObject Prefab;
}

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    private int _initPoolSize = 10;
    [SerializeField] private EnemyPrefabPair[] _enemyPrefabairs;
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
        foreach (EnemyPrefabPair pair in _enemyPrefabairs)
        {
            if (_enemyPools.ContainsKey(pair.Type)) continue;
            ObjectPool pool = new ObjectPool();
            pool.InitPool(pair.Prefab, _initPoolSize, this.transform);
            _enemyPools.Add(pair.Type, pool);
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
