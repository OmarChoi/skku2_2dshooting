using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChasingMovement : EnemyMovement
{
    private GameObject _target = null;
    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        SetDirection();
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void SetDirection()
    {
        if (_target == null) return;
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = _target.transform.position;
        _direction = targetPosition - myPosition;
        _direction.Normalize();

        float defaultAngle = Mathf.Atan2(Vector2.down.y, Vector2.down.x);
        float targetAngle = Mathf.Atan2(_direction.y, _direction.x);
        float finalAngle = (targetAngle - defaultAngle) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
    }
}
