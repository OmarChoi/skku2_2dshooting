using UnityEngine;

public enum EItemType
{
    MoveSpeedUp,
    Heal,
    AttackSpeedUp
}

public abstract class ItemType : MonoBehaviour
{
    protected abstract void ApplyEffect(ref Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyEffect(ref other);
    }
}
