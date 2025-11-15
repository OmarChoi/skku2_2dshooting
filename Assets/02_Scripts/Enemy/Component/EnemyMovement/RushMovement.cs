using UnityEngine;

public class RushMovement : EnemyMovement
{
    private float _createTime;
    private float _waitTime = 3.0f;
    private float _rushSpeed = 12.0f;

    private void Start()
    {
        _speed = _rushSpeed;
        _createTime = Time.time;
    }

    private void SetDirection()
    {
        GameObject target = GameObject.FindWithTag("Player");
        if (target == null) return;

        Vector3 myPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        _direction = targetPosition - myPosition;
        _direction.Normalize();

        float defaultAngle = Mathf.Atan2(Vector2.down.y, Vector2.down.x);
        float targetAngle = Mathf.Atan2(_direction.y, _direction.x);
        float finalAngle = (targetAngle - defaultAngle) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
    }

    protected override void Move()
    {
        if(CanMove() == false) return;
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private bool CanMove()
    {
        float currentTime = Time.time;
        if (currentTime - _createTime > _waitTime) return true;
        SetDirection();
        return false;
    }

    public override void Init()
    {
        _speed = _rushSpeed;
        _createTime = Time.time;
        _direction = Vector3.down;
    }
}
