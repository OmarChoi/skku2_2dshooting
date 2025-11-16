using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [Header("이동속도")]
    public float StartSpeed = 1.0f;
    public float EndSpeed = 7.0f;
    public float AccelerationTime = 1.2f;

    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _damage = 40.0f;

    [SerializeField]
    private int _poolKey;
    public int PoolKey => _poolKey;

    private void Start()
    {
        Init();
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
        // AccelationTime 시간 안에 최대 속도에 도달합니다.
        float acceleration = ((EndSpeed - StartSpeed) / AccelerationTime);
        _speed += acceleration * Time.deltaTime;
        _speed = Mathf.Min(_speed, EndSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        IDamageable damagable = other.GetComponent<IDamageable>();
        if (damagable == null) return;
        damagable.TakeDamage(_damage);
        Release();
    }

    public void Init()
    {
        _speed = StartSpeed;
    }

    public void Release()
    {
        BulletFactory.Instance.ReleaseBullet(this.gameObject);
    }

    public void SetPoolKey(int key) => _poolKey = key;
}
