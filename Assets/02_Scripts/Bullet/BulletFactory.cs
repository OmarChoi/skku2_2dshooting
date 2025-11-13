using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }


    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    public GameObject MakeBullet(Vector3 position)
    {
        return Instantiate(BulletPrefab, position, Quaternion.identity, this.transform);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(SubBulletPrefab, position, Quaternion.identity, this.transform);
    }
}
