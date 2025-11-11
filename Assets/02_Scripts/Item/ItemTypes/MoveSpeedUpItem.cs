using UnityEngine;

public class MoveSpeedUpItem : ItemType
{
    private int _increaseAmount = 1;
    public GameObject SpeedUpEffectPrefab;

    protected override void ApplyEffect(ref Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;
        if (SpeedUpEffectPrefab != null)
        {
            Instantiate(SpeedUpEffectPrefab, other.transform);
        }
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.IncreaseSpeed(_increaseAmount);
        Destroy(gameObject);
    }
}
