using UnityEngine;

public enum EItemType
{
    MoveSpeedUp,
    Heal,
    AttackSpeedUp
}

public abstract class ItemType : MonoBehaviour
{
    public GameObject EffectPrefab = null;
    protected abstract void ApplyEffect(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyEffect(other);
    }

    protected void SpawnEffect(Transform transform)
    {
        if (EffectPrefab == null) return;
        Instantiate(EffectPrefab, transform);
    }
}
