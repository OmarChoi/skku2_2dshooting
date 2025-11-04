using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("총구")]
    public Transform[] FirePosition;

    [Header("쿨타임")]
    public float CoolTime = 0.6f;   // 몇초마다 발사가 가능한지
    private float _elapsedTime = 0.0f;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            // 쿨타임 확인
            if (_elapsedTime > CoolTime)
            {
                _elapsedTime = 0.0f;
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        for (int i = 0; i < FirePosition.Length; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.transform.position = FirePosition[i].position;
        }
    }
}
