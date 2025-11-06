using UnityEngine;

public class Enemy : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerHealth playerState = other.GetComponent<PlayerHealth>();
        if (playerState == null) return;

        playerState.TakeDamage();
        Die();
    }
}
