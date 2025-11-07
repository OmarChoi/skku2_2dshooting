using UnityEngine;

public class HealItem : ItemType
{
    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Heal();
        Destroy(gameObject);
    }
}
