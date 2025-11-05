using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("총구")]
    public Transform LeftFirePosition;
    public Transform RightFirePosition;
    public Transform LeftSubFirePosition;
    public Transform RightSubFirePosition;

    [Header("총알 쿨타임")]
    public float MainBulletCoolTime = 0.6f;   // 몇초마다 발사가 가능한지
    public float SubBulletCoolTime = 0.4f;    // 서브 총알이 몇초마다 발사가 가능한지

    [Header("발사 방식")]
    public bool IsAutoFire = false;

    private float _mainFireTimer = 0.0f;
    private float _subFireTimer = 0.0f;

    private void Update()
    {
        GetFireType();
        ProcessMainBullets();
        ProcessSubBullets();
    }

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

    private void ProcessMainBullets()
    {
        _mainFireTimer += Time.deltaTime;
        if (_mainFireTimer < MainBulletCoolTime) return;

        if (Input.GetKey(KeyCode.Space) || IsAutoFire)
        {
            _mainFireTimer = 0.0f;
            Instantiate(BulletPrefab, LeftFirePosition.position, LeftFirePosition.rotation);
            Instantiate(BulletPrefab, RightFirePosition.position, RightFirePosition.rotation);
        }
    }

    private void ProcessSubBullets()
    {
        _subFireTimer += Time.deltaTime;
        if (_subFireTimer < SubBulletCoolTime) return;
        if (!IsAutoFire) return;

        _subFireTimer = 0.0f;
        Instantiate(SubBulletPrefab, LeftSubFirePosition.position, LeftSubFirePosition.rotation);
        Instantiate(SubBulletPrefab, RightSubFirePosition.position, RightSubFirePosition.rotation);
    }
}
