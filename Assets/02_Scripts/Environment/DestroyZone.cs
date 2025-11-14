using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletFactory.Instance.ReleaseBullet(other.gameObject);
            return;
        }
        Destroy(other.gameObject);
    }
}
