using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총구")]
    public Transform LeftFirePosition;
    public Transform RightFirePosition;
    public Transform LeftSubFirePosition;
    public Transform RightSubFirePosition;

    [Header("총알 쿨타임")]
    // 몇초마다 발사가 가능한지 의미합니다.
    private float _mainBulletCoolTime = 0.6f;
    // 서브 총알이 몇초마다 발사가 가능한지 의미합니다.
    private float _subBulletCoolTime = 0.4f;
    private float _attackSpeed = 1.0f;
    private float _maxAttackSpeed = 2.5f;
    private float _mainFireTimer = 0.0f;
    private float _subFireTimer = 0.0f;

    [Header("발사 방식")]
    private bool _isAutoFire = false;

    [Header("사운드")]
    public AudioSource FireSound;

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
            FireSound.Play();

            _mainFireTimer = 0.0f;
            BulletFactory.Instance.MakeBullet(EBulletType.Main, LeftFirePosition.position);
            BulletFactory.Instance.MakeBullet(EBulletType.Main, RightFirePosition.position);
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
            BulletFactory.Instance.MakeBullet(EBulletType.Sub, LeftSubFirePosition.position);
            BulletFactory.Instance.MakeBullet(EBulletType.Sub, RightSubFirePosition.position);
        }
    }

    public void IncreaseAttackSpeedRatio(float ratio)
    {
        _attackSpeed *= ratio;
        _attackSpeed = Mathf.Min(_attackSpeed, _maxAttackSpeed);
    }
}
