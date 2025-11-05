using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("체력")]
    private int _life = 3;
    public int Life
    {
        get { return _life; }
        set
        {
            _life = value;
            if (_life == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
