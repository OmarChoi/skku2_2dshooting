using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동속도")]
    private float _speed = 1.0f;
    public float StartSpeed = 1.0f;
    public float EndSpeed = 7.0f;
    public float Accelation = 1.2f;

    private void Start()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        _speed += Accelation * Time.deltaTime;
        _speed = Mathf.Min(_speed, EndSpeed);
        //          ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.

        Vector2 direction = Vector2.up;
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
