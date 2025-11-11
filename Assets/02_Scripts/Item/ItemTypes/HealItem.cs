using UnityEngine;

public class HealItem : ItemType
{
    public GameObject HealEffectPrefab;

    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        if (HealEffectPrefab != null)
        {
            Instantiate(HealEffectPrefab, other.transform);
        }
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Heal();
        Destroy(gameObject);
    }
}
