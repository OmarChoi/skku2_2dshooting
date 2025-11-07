using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("능력치")]
    private float _health = 100.0f;

    public void TakeDamage(float damage)
    {
        _health -= damage;
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
        Destroy(gameObject);
    }
}
