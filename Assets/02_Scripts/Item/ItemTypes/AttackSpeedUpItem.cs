using UnityEngine;

public class AttackSpeedUpItem : ItemType
{
    private float _increaseMiltiply = 1.2f;
    public GameObject AttackSpeedUpEffectPrefab;

    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        if (AttackSpeedUpEffectPrefab != null)
        {
            Instantiate(AttackSpeedUpEffectPrefab, other.transform);
        }
        PlayerFire playerFire = other.GetComponent<PlayerFire>();
        playerFire.IncreaseAttackSpeedRatio(_increaseMiltiply);
        Destroy(gameObject);
    }
}
