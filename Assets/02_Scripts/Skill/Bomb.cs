using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _damage = 1000.0f;

    [Header("크기")]
    private float _startSize = 0.0f;
    private float _maxSize = 2.0f;

    [Header("시간")]
    private float _elapsedTime = 0.0f;
    private float _duration = 3.0f;
    private float _maxSizeReachedTime = 2.0f;  // 최대 크기 도달 시간

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        CheckDuration();
        UpdateSize();
    }

    private void CheckDuration()
    {
        if (_elapsedTime <= _duration) return;
        Destroy(gameObject);
    }

    private void UpdateSize()
    {
        float maxTime = Mathf.Min(_duration, _maxSizeReachedTime);
        float scaleSize = Mathf.Lerp(_startSize, _maxSize, _elapsedTime / maxTime);
        transform.localScale = Vector3.one * scaleSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null) return;
        enemy.TakeDamage(_damage);
    }
}
