using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _player = null;

    [Header("스폰 시간")]
    [SerializeField]
    private float _spawnTime = 0.5f;
    private float _spawnTimer = 0.0f;

    [Header("스폰 쿨타임")]
    private float _minSpawnCoolTime = 1.0f;
    private float _maxSpawnCoolTime = 3.0f;

    [Header("스폰 확률")]
    private int _totalWeight = 0;
    private int[] _probabilityWeights = new int[] { 60, 30, 30 };

    [Header("스폰시 위치 오프셋")]
    private float _minSpawnX = -2.25f;
    private float _maxSpawnX = 2.25f;
    private float _minYDistance = 3.0f;
    private float _maxSpawnY = 5.0f;

    [Header("보스")]
    private int _bossSpawnScore = 500;
    private bool _bossSpawned = false;

    private void Start()
    {
        foreach (int weight in _probabilityWeights)
        {
            _totalWeight += weight;
        }
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (CanSpawn() == false) return;
        SpawnEnemy();
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (_bossSpawned == true) return;
        if (ScoreManager.Instance.CurrentScore >= _bossSpawnScore)
        {
            EnemyFactory.Instance.SpawnBoss();
            _bossSpawned = true;
        }
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
        EEnemyType type = (EEnemyType)Utils.GetRandomIndexByWeight(_totalWeight, _probabilityWeights);
        GameObject enemy = EnemyFactory.Instance.SpawnEnemy(type, this.transform.position);
        if (type == EEnemyType.Rush)
        {
            if (_player == null) return;
            Vector2 spawnPosition = _player.transform.position;
            spawnPosition.x = UnityEngine.Random.Range(_minSpawnX, _maxSpawnX);
            spawnPosition.y = UnityEngine.Random.Range(spawnPosition.y + _minYDistance, _maxSpawnY);
            enemy.transform.position = spawnPosition;
        }
        enemy.GetComponent<EnemyMovement>().Init();
    }
}
