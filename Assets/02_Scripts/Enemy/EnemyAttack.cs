using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerHealth playerState = other.GetComponent<PlayerHealth>();
        if (playerState == null) return;

        playerState.TakeDamage(); 
        Destroy(gameObject);
    }
}
