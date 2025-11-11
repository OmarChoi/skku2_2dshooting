using UnityEngine;

public class HealItem : ItemType
{
    protected override void ApplyEffect(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        SpawnEffect(other.transform);
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Heal();
        Destroy(gameObject);
    }
}
