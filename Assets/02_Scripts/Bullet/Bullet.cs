using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동속도")]
    public float StartSpeed = 1.0f;
    public float EndSpeed = 7.0f;
    public float AccelerationTime = 1.2f;

    private Vector2 _startPosition;
    private float _speed = 1.0f;

    private void Start()
    {
        _speed = StartSpeed;
        _startPosition = transform.position;
    }

    protected void Update()
    {
        Accelerate();
        Move();
    }

    protected virtual void Move()
    {
        Vector2 direction = Vector2.up;
        UpdatePosition(direction);
    }

    protected void UpdatePosition(Vector2 direction)
    {
        direction.Normalize();
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void Accelerate()
    {
        // AccelationTime 시간 안에 최대 속도에 도달하게 구현
        float acceleration = ((EndSpeed - StartSpeed) / AccelerationTime);
        _speed += acceleration * Time.deltaTime;
        _speed = Mathf.Min(_speed, EndSpeed);
    }
}
