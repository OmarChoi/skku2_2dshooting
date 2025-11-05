using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("능력치")]
    public float Speed = 3.0f;
    private float _health = 100.0f;
    
    void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        Vector3 direction = Vector3.down;
        transform.position += direction * (Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerState playerState = other.GetComponent<PlayerState>();
        if (playerState == null) return;

        playerState.TakeDamage();
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
