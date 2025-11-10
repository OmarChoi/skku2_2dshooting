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
    private float _mainBulletCoolTime = 0.6f;   // 몇초마다 발사가 가능한지
    private float _subBulletCoolTime = 0.4f;    // 서브 총알이 몇초마다 발사가 가능한지
    private float _attackSpeed = 1.0f;
    private float _maxAttackSpeed = 2.5f;

    [Header("발사 방식")]
    private bool _isAutoFire = false;

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
            _isAutoFire = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isAutoFire = false;
        }
    }

    private void ProcessMainBullets()
    {
        _mainFireTimer += Time.deltaTime;
        float fireTimer = _mainFireTimer * _attackSpeed;
        if (fireTimer < _mainBulletCoolTime) return;

        if (Input.GetKey(KeyCode.Space) || _isAutoFire)
        {
            _mainFireTimer = 0.0f;
            Instantiate(BulletPrefab, LeftFirePosition.position, LeftFirePosition.rotation);
            Instantiate(BulletPrefab, RightFirePosition.position, RightFirePosition.rotation);
        }
    }

    private void ProcessSubBullets()
    {
        _subFireTimer += Time.deltaTime;
        float fireTimer = _subFireTimer * _attackSpeed;
        if (fireTimer < _subBulletCoolTime) return;
        if (Input.GetKey(KeyCode.Space) || _isAutoFire)
        {
            _subFireTimer = 0.0f;
            Instantiate(SubBulletPrefab, LeftSubFirePosition.position, LeftSubFirePosition.rotation);
            Instantiate(SubBulletPrefab, RightSubFirePosition.position, RightSubFirePosition.rotation);
        }
    }

    public void IncreaseAttackSpeedRatio(float ratio)
    {
        _attackSpeed *= ratio;
        _attackSpeed = Mathf.Min(_attackSpeed, _maxAttackSpeed);
    }
}
