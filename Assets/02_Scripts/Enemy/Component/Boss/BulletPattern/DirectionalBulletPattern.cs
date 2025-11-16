using UnityEngine;

public class DirectionalBulletPatern : IBulletPattern
{
    private float _spreadAngle = 15.0f;
    private int _bulletCount = 5;

    public void ExecuteBullet(Vector3 startPosition)
    {
        Vector3 baseDirection = Vector3.down;
        float half = (_bulletCount - 1) * 0.5f;

        for (int i = 0; i < _bulletCount; ++i)
        {
            float offsetIndex = i - half;
            float angle = offsetIndex * _spreadAngle;

            Quaternion offsetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 direction = offsetAngle * baseDirection;

            GameObject bullet = BulletFactory.Instance.MakeBullet(EBulletType.BossDirectional, startPosition, direction);
        }
    }
}
