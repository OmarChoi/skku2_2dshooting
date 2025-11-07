using UnityEngine;

public class MoveSpeedUpItem : ItemType
{
    private int _increaseAmount = 1;

    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.IncreaseSpeed(_increaseAmount);
        Destroy(gameObject);
    }
}
