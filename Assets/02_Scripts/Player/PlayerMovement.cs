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
    public float Speed = 3.0f;
    public float MaxSpeed = 10.0f;
    public float MinSpeed = 1.0f;
    public float SpeedChangeAmount = 1.0f;

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
        // 처음 시작 위치 저장
        _originPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed = Mathf.Min(Speed + SpeedChangeAmount, MaxSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed = Mathf.Max(Speed - SpeedChangeAmount, MinSpeed);
        }

        float currentSpeed = Speed;
        // LeftShift를 누르고 있으면 가속도 만큼 속도가 증가한다.
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            currentSpeed *= BoostAcceleration;
        }

        // R키를 누르고 있으면 0, 0으로 이동한다.
        // 누르고 있지 않으면 키 입력에 따라 움직인다.
        if (Input.GetKey(KeyCode.R))
        {
            TranslateToOrigin(currentSpeed);
            return;
        }

        // 1. 키보드 입력을 갑지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든 것을 담당한다.
        Vector2 direction = new Vector2(0, 0);
        Vector2 position = transform.position;

        float h = Input.GetAxisRaw("Horizontal");  // 수평 입력에 대한 값을 -1, 0, 1로 가져 온다.
        float v = Input.GetAxisRaw("Vertical");    // 수직 입력에 대한 값을 -1, 0, 1로 가져 온다.

        // 2. 입력으로부터 방향을 구한다.
        // 벡터 : 크기와 방향을 표현하는 물리 개념
        direction = new Vector2(h, v);
        direction.Normalize();

        // 그 방향으로 이동을 한다.
        // 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2 newPosition = position + direction * currentSpeed * Time.deltaTime;

        // 범위를 넘어가면 min, max 값으로 고정
        // x축 기준으로 넘어가면 반대 방향에서 나오게 설정
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

        // Time.deltaTime : 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지 나타내는 값
        transform.position = newPosition;
    }

    private void TranslateToOrigin(float speed)
    {
        // 2. 방향을 구한다.
        Vector2 direction = _originPosition - (Vector2)transform.position;

        // 3. 이동을 한다.
        transform.Translate(direction * speed *  Time.deltaTime);
    }
}
