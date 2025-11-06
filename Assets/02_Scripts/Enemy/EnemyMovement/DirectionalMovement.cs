using UnityEngine;

public class DirectionalMovement : EnemyMovement
{
    protected override void SetDirection()
    {
        _direction = Vector3.down;
    }
}
