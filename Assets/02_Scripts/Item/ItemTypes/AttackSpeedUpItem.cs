using UnityEngine;

public class AttackSpeedUpItem : ItemType
{
    private float _increaseMultiply = 1.2f;

    protected override void ApplyEffect(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        SpawnEffect(other.transform);
        PlayerFire playerFire = other.GetComponent<PlayerFire>();
        playerFire.IncreaseAttackSpeedRatio(_increaseMultiply);
        Destroy(gameObject);
    }
}
