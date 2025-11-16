using UnityEngine;
using DG.Tweening;

public class BossMovement : MonoBehaviour
{
    private Vector2 _spawnPosition = new Vector2(0, 7.5f);
    private Vector2 _destination = new Vector2(0, 2.5f);
    private float _appearanceTime = 3.0f;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        this.transform.position = _spawnPosition;
        Move();
    }

    private void Move()
    {
        this.transform.DOMove(_destination, _appearanceTime);
    }
}
