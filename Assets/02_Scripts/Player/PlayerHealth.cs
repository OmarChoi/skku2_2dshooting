using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    private int _life = 3;

    public void TakeDamage()
    {
        --_life;
        if (_life == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
