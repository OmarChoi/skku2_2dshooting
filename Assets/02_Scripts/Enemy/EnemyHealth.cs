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
        Destroy(gameObject);
    }
}
