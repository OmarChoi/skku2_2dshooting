using UnityEngine;

public class MainBullet : BulletBase
{
    [Header("이동속도")]
    private float _endSpeed = 7.0f;
    private float _accelerationTime = 1.2f;

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
        Accelerate();
        UpdatePosition();
    }

    protected void UpdatePosition()
    {
        Vector2 position = transform.position;
        Vector2 newPosition = position + Direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void Accelerate()
    {
        float acceleration = ((_endSpeed - _startSpeed) / _accelerationTime);
        _speed += acceleration * Time.deltaTime;
        _speed = Mathf.Min(_speed, _endSpeed);
    }
}
