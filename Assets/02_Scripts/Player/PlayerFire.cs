using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("총구")]
    public Transform[] FirePosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < FirePosition.Length; i++)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = FirePosition[i].position;
            }
        }
    }
}
