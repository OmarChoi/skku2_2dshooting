using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("총구")]
    public Transform[] MainFirePosition;
    public Transform[] SubFirePosition;

    [Header("총알 쿨타임")]
    public float MainBulletCoolTime = 0.6f;   // 몇초마다 발사가 가능한지
    public float SubBulletCoolTime = 0.4f;    // 서브 총알이 몇초마다 발사가 가능한지

    [Header("발사 방식")]
    public bool IsAutoFire = true;

    private float _mainFireTimer = 0.0f;
    private float _subFireTimer = 0.0f;

    private void Update()
    {
        _mainFireTimer += Time.deltaTime;
        _subFireTimer += Time.deltaTime;

        GetFireType();
        ProcessMainBullet();
        ProcessSubBullet();
    }

    private bool CheckMainCoolTime => _mainFireTimer >= MainBulletCoolTime;
    private bool CheckSubCoolTime => _subFireTimer >= SubBulletCoolTime;

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

    private void ProcessMainBullet()
    {
        bool isKeyInput = Input.GetKey(KeyCode.Space) || IsAutoFire;
        if (isKeyInput && CheckMainCoolTime == true)
        {
            _mainFireTimer = 0.0f;
            for (int i = 0; i < MainFirePosition.Length; i++)
            {
                GameObject bullet = Instantiate(BulletPrefab, MainFirePosition[i]);
            }
        }
    }

    private void ProcessSubBullet()
    {
        if (CheckSubCoolTime == true)
        {
            _subFireTimer = 0.0f;
            for (int i = 0; i < SubFirePosition.Length; i++)
            {
                GameObject bullet = Instantiate(SubBulletPrefab, SubFirePosition[i]);
            }
        }
    }
}
