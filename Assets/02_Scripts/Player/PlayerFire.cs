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

    [Header("발사 방식")]
    public bool IsAutoFire = true;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        GetFireType();
        if (CheckCoolTime == true)
        {
            if (Input.GetKey(KeyCode.Space) || IsAutoFire)
            {
                _elapsedTime = 0.0f;
                FireBullet();
            }
        }
    }

    private bool CheckCoolTime => _elapsedTime >= CoolTime;

    private void GetFireType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IsAutoFire = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IsAutoFire = false;
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
