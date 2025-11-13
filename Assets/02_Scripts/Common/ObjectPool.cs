using UnityEngine;
using System.Collections.Generic;

public class ObjectPool
{
    private Queue<GameObject> _pool;
    
    private GameObject _prefab = null;
    private Transform _parent = null;

    private int _poolCount;
    private float _increasePoolRate = 1.5f;

    private void CreateObject(int newObjectCount)
    {
        for (int i = 0; i < newObjectCount; ++i)
        {
            GameObject newObject = GameObject.Instantiate(_prefab, _parent);
            newObject.SetActive(false);
            _pool.Enqueue(newObject);
        }
    }

    public void InitPool(GameObject prefab,  int poolCount, Transform parent = null)
    {
        _poolCount = poolCount;
        _prefab = prefab;
        _parent = parent;
        _pool = new Queue<GameObject>(poolCount);

        CreateObject(poolCount);
    }

    public GameObject GetObject()
    {
        if (_pool.Count == 0)
        {
            int increaseCount = (int)(_poolCount * _increasePoolRate);
            CreateObject(increaseCount);
            _poolCount += increaseCount;
        }

        GameObject pooledObject = _pool.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReleaseObject(GameObject releaseObject)
    {
        releaseObject.SetActive(false);
        _pool.Enqueue(releaseObject);
    }
}
