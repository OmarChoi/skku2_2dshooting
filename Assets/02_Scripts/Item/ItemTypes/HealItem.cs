using UnityEngine;

public class HealItem : ItemType
{
    private float _healAmount = 10.0f;

    protected override void ApplyEffect(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        SpawnEffect(other.transform);
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Heal(_healAmount);
        Destroy(this.gameObject);
    }
}
