using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Info")]
    public float Speed = 3.0f;
    private float _helth = 100.0f;
    public float Health
    {
        get { return _helth; }
        set
        {
            _helth = value;
            if (_helth < float.Epsilon)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = Vector3.down;
        transform.position += direction * (Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerState playerState = other.GetComponent<PlayerState>();
        if (playerState == null) return;
        playerState.Life -= 1;
        Destroy(gameObject);
    }
}
