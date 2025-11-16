using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float _damage = 30.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        IDamageable playerState = other.GetComponent<IDamageable>();
        if (playerState == null) return;
        playerState.TakeDamage(_damage);
        
        GetComponent<IPoolable>().Release();
    }
}
