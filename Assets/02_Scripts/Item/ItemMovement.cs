using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    private GameObject _target;
    private float _elapsedTime = 0.0f;
    private float _waitTime = 2.0f;
    private float _speed = 3.0f;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (CanMove() == false) return;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 direction = Vector3.down;
        if (_target == null)
        {
            transform.position += direction * (_speed * Time.deltaTime);
            return;
        }
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = _target.transform.position;
        direction = targetPosition - myPosition;
        direction.Normalize();
        transform.position += direction * (_speed * Time.deltaTime);
    }

    private bool CanMove()
    {
        _elapsedTime += Time.deltaTime;
        return _elapsedTime >= _waitTime;
    }
}
