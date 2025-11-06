using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    [SerializeField]
    private GameObject _enemyPrefab;

    [Header("스폰 시간")]
    [SerializeField]
    private float _spawnTime = 0.5f;
    private float _spawnTimer = 0.0f;

    [Header("스폰 쿨타임")]
    private float _minSpawnCoolTime = 1.0f;
    private float _maxSpawnCoolTime = 3.0f;

    private void Update()
    {
        if (CanSpawn() == false) return;
        SpawnEnemy();
    }

    private bool CanSpawn()
    {
        _spawnTimer += Time.deltaTime;
        return _spawnTimer >= _spawnTime;
    }

    private void ResetCoolTime()
    {
        _spawnTimer = 0.0f;
        _spawnTime = UnityEngine.Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
    }

    private void SpawnEnemy()
    {
        ResetCoolTime();
        GameObject enemyObject = Instantiate(_enemyPrefab, transform.position, transform.rotation);
        enemyObject.AddComponent<ChasingMovement>();
    }
}
