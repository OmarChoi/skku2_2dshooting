using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    private int _life = 3;
    private int _maxLife = 3;

    [SerializeField] private GameObject _gameOverPrefab;

    public void TakeDamage()
    {
        --_life;
        if (_life == 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        _life = Mathf.Min(_life + 1, _maxLife);
    }

    private void Die()
    {
        Instantiate(_gameOverPrefab);
        Destroy(gameObject);
    }
}
