using UnityEngine;

public class EnemyHealth : MonoBehaviour, IPoolable, IDamageable
{
    [Header("능력치")]
    private float _health = 100.0f;
    private float _maxHealth = 100.0f;

    private Animator _animator = null;

    [Header("이펙트 프리팹")]
    public GameObject ExplosionPrefab;

    private int _poolKey = 0;
    public int PoolKey => _poolKey;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject itemSpawner = GameObject.FindWithTag("ItemSpawner");
        if (itemSpawner != null)
        {
            ItemSpawner spawner = itemSpawner.GetComponent<ItemSpawner>();
            spawner.SpawnItem(transform.position);
        }

        ScoreManager.Instance.AddScore(100);
        MakeExplosionEffect();
        Release();
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

    public void Init()
    {
        _health = _maxHealth;
    }

    public void Release()
    {
        EnemyFactory.Instance.ReleaseEnemy(this.gameObject);
    }

    public void SetPoolKey(int key) => _poolKey = key;
}
