using UnityEngine;

public class DirectionalMove : EnemyMove
{
    protected override void SetDirection()
    {
        _direction = Vector3.down;
    }
}
