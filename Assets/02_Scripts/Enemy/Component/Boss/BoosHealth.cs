using UnityEngine;

public class BoosHealth : MonoBehaviour
{
    private float _health;
    private float _maxHealth;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _health = _maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
