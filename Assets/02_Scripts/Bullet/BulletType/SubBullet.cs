using UnityEngine;

public class SubBullet : BulletBase
{
    protected override void ApplyDamage(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable == null) return;
        damageable.TakeDamage(_damage);
        Release();
    }

    protected override void Move()
    {
        UpdatePosition();
    }

    protected void UpdatePosition()
    {
        Vector2 position = transform.position;
        Vector2 newPosition = position + Direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
