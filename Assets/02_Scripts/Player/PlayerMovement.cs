using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 목표
    // "키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다.

    // 구현 순서
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    // 구현 속성
    [Header("속도")]
    private float _speed = 3.0f;
    private float _maxSpeed = 10.0f;
    private float _minSpeed = 1.0f;

    [Header("가속")]
    public bool UseBoost = false;
    public float BoostAcceleration = 1.2f;

    [Header("시작위치")]
    private Vector2 _originPosition = new Vector2(0.0f, 0.0f);

    [Header("이동범위")]
    public float MinX = -2.85f;
    public float MaxX = 2.85f;
    public float MinY = -4.5f;
    public float MaxY = 0.0f;

    private void Start()
    {
        _originPosition = transform.position;
    }

    private void Update()
    {
        float currentSpeed = _speed;
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            currentSpeed *= BoostAcceleration;
        }

        if (Input.GetKey(KeyCode.R))
        {
            TranslateToOrigin(currentSpeed);
            return;
        }

        Vector2 direction = new Vector2(0, 0);
        Vector2 position = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);
        direction.Normalize();

        Vector2 newPosition = position + direction * currentSpeed * Time.deltaTime;

        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        if (newPosition.y < MinY)
        {
            newPosition.y = MinY;
        }
        else if (newPosition.y > MaxY)
        {
            newPosition.y = MaxY;
        }

        transform.position = newPosition;
    }

    public void IncreaseSpeed(int value)
    {
        _speed = Mathf.Min(_speed + value, _maxSpeed);
    }

    public void DecreaseSpeed(int value)
    {
        _speed = Mathf.Max(_speed - value, _minSpeed);
    }

    private void TranslateToOrigin(float speed)
    {
        Vector2 direction = _originPosition - (Vector2)transform.position;
        transform.Translate(direction * speed *  Time.deltaTime);
    }
}
