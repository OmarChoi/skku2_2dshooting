using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("체력")]
    private float _health = 100;
    private float _maxhealth = 100;

    [SerializeField] private GameObject _gameOverPrefab;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        _health = Mathf.Min(_health + healAmount, _maxhealth);
    }

    private void Die()
    {
        Instantiate(_gameOverPrefab);
        Destroy(gameObject);
    }
}
