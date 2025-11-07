using UnityEngine;

public class AttackSpeedUpItem : ItemType
{
    private float _increaseMiltiply = 1.2f;
    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        PlayerFire playerFire = other.GetComponent<PlayerFire>();
        playerFire.IncreaseAttackSpeedRatio(_increaseMiltiply);
        Destroy(gameObject);
    }
}
