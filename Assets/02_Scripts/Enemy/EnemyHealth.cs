using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("능력치")]
    private float _health = 100.0f;

    private Animator _animator = null;

    [Header("이펙트 프리팹")]
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
        Destroy(gameObject);
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}
