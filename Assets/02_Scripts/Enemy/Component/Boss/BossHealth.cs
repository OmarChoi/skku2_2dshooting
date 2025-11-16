using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    private float _health = 5000.0f;
    private float _maxHealth = 5000.0f;
    private Animator _animator = null;
    public GameObject ExplosionPrefab;

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
        EnemyFactory.Instance.ReleaseBoss();
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}
