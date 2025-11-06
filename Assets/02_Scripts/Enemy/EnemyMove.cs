using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    private float _speed = 3.0f;
    protected Vector3 _direction = Vector3.down;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        SetDirection();
        UpdatePosition();
    }

    protected abstract void SetDirection();

    protected void UpdatePosition()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }
}
