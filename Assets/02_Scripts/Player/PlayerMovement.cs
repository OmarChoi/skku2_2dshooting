using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EPlayerState
{
    Idle,
    Move,
}

public class PlayerMovement : MonoBehaviour
{
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


    [Header("자동 이동")]
    private GameObject _target = null;
    private bool _isAutoMove = false;
    private float _stopDistance = 0.2f;
    private EPlayerState _playerState = EPlayerState.Idle;
    private Dictionary<EPlayerState, Action> _stateActions;

    [Header("애니메이션")]
    private Animator _animator;

    private void Start()
    {
        _originPosition = transform.position;
        _stateActions = new Dictionary<EPlayerState, Action>
        {
            { EPlayerState.Idle, FindTarget },
            { EPlayerState.Move,  MoveToTarget },
        };
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAutoMode();
        if (_isAutoMove == true)
        {
            UpdateAutoState();
            return;
        }

        float currentSpeed = _speed;
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            currentSpeed *= BoostAcceleration;
        }
        UpdatePosition(currentSpeed);
    }

    private void CheckAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoMove = false;
        }
    }

    public void UpdatePosition(float speed)
    {
        Vector2 direction = new Vector2(0, 0);
        Vector2 position = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);
        direction.Normalize();

        _animator.SetInteger("x", (int)direction.x);
        // if (direction.x < 0) _animator.Play("Left");
        // else if (direction.x < 0) _animator.Play("Right");
        // else _animator.Play("Idle");

        Vector2 newPosition = position + direction * speed * Time.deltaTime;

        float width = MaxX - MinX;
        newPosition.x = MinX + Mathf.Repeat(newPosition.x - MinX, width);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
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

    public void UpdateAutoState()
    {
        if (_target == null)
        {
            _playerState = EPlayerState.Idle;
        }
        _stateActions[_playerState]?.Invoke();
    }

    private void MoveToTarget()
    {
        if (_target == null) return;
        Vector2 targetPosition = _target.transform.position;
        Vector2 myPosition = transform.position;
        Vector2 toTarget = targetPosition - myPosition;
        if (Mathf.Abs(toTarget.x) < _stopDistance) return;

        float xDirection = GetXDirection(myPosition.x, targetPosition.x);
        float yDistance = Mathf.Abs(toTarget.y);
        float yDirection = -Mathf.Sign(toTarget.y); // 적이 아래에 있으면 위쪽으로 회피하게 설정
        float xSpeed = yDistance * _speed;          // y좌표가 가까우면 피격 당하지 않기 위해 천천히 이동
        Vector2 moveDirection = new Vector2(Mathf.Sign(xDirection) * xSpeed, yDirection).normalized;

        _animator.SetInteger("x", (int)moveDirection.x);
        Vector2 newPosition = myPosition + moveDirection * _speed * Time.deltaTime;
        CheckPositionInRange(ref newPosition);
        transform.position = newPosition;
    }

    private float GetXDirection(float currentX, float targetX)
    {
        // 순간이동 해서 이동하는게 더 빠른지 확인
        float width = MaxX - MinX;
        float distance = Mathf.Abs(targetX - currentX);
        float direction = targetX - currentX;
        if (distance > width * 0.5f)
        {
            direction = -direction;
        }
        return direction;
    }

    private void CheckPositionInRange(ref Vector2 newPosition)
    {
        float width = MaxX - MinX;
        newPosition.x = MinX + Mathf.Repeat(newPosition.x - MinX, width);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
    }

    private void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0) return;

        float minDistance = float.MaxValue;
        foreach (GameObject target in targets)
        {
            Vector2 targetPosition = target.transform.position;
            Vector2 myPosition = transform.position;
            float distance = (targetPosition - myPosition).magnitude;
            if (targetPosition.y < myPosition.y) continue;
            if (distance < minDistance)
            {
                minDistance = distance;
                _target = target;
            }
        }

        TranslateToOrigin();
        if (_target != null)
        {
            _playerState = EPlayerState.Move;
        }
    }

    private void TranslateToOrigin()
    {
        Vector2 direction = _originPosition - (Vector2)transform.position;
        transform.Translate(direction * _speed * Time.deltaTime);

        _animator.SetInteger("x", (int)direction.x);

    }
}
