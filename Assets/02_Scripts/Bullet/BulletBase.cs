using UnityEngine;

public abstract class BulletBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected float _speed = 1.0f;
    [SerializeField] protected float _startSpeed = 1.0f;

    [SerializeField]
    protected float _damage = 40.0f;
    private Vector2 _direction = Vector2.up;
    public Vector2 Direction => _direction;

    [SerializeField] private int _poolKey;

    public int PoolKey => _poolKey;

    private void Start()
    {
        _startSpeed = _speed;
        Init();
    }

    protected void Update()
    {
        Move();
    }

    protected abstract void Move();
    protected abstract void ApplyDamage(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other);
    }

    public void Init()
    {
        _speed = _startSpeed;
    }

    public void Release()
    {
        BulletFactory.Instance.ReleaseBullet(this.gameObject);
    }

    public void SetPoolKey(int key) => _poolKey = key;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }
}
