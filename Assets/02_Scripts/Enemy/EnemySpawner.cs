using UnityEngine;

public enum EMovementType
{
    DirectionalMovement,
    ChasingMovement,
}

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    [SerializeField]
    private GameObject[] _enemyPrefab;

    [Header("스폰 시간")]
    [SerializeField]
    private float _spawnTime = 0.5f;
    private float _spawnTimer = 0.0f;

    [Header("스폰 쿨타임")]
    private float _minSpawnCoolTime = 1.0f;
    private float _maxSpawnCoolTime = 3.0f;

    [Header("스폰 확률")]
    private int _totalWeight = 0;
    private int[] _probabilityWeights = new int[] { 70, 30 };

    private void Start()
    {
        foreach (int weight in _probabilityWeights)
        {
            _totalWeight += weight;
        }
    }

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
        EMovementType type = GetMovementType();
        if (type == EMovementType.DirectionalMovement)
        {
            Instantiate(_enemyPrefab[(int)EMovementType.DirectionalMovement], transform.position, transform.rotation);
        }
        else if (type == EMovementType.ChasingMovement)
        {
            Instantiate(_enemyPrefab[(int)EMovementType.ChasingMovement], transform.position, transform.rotation);
        }
    }

    private EMovementType GetMovementType()
    {
        float randomValue = UnityEngine.Random.value;
        float totalValue = 0.0f;
        int type = 0;
        for (int i = 0; i < _probabilityWeights.Length; ++i)
        {
            totalValue += (float)_probabilityWeights[i] / _totalWeight;
            if (randomValue < totalValue)
            {
                type = i;
                break;
            }
        }
        return (EMovementType)type;
    }
}
