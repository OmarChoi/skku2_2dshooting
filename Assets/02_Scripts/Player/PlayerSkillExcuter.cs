using UnityEngine;

public class PlayerSkillExcuter : MonoBehaviour
{
    [Header("스킬 프리팹")]
    public GameObject BombSkillPrefab;

    [Header("Bomb 스킬")]
    private float _bombSkillCoolTime = 5.0f;
    private float _bombSkillTimer = 0.0f;

    private void Update()
    {
        ProcessInput();
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _bombSkillTimer = Mathf.Max(_bombSkillTimer - Time.deltaTime, 0.0f);
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_bombSkillTimer > float.Epsilon) return;
            if (BombSkillPrefab == null) return;
            _bombSkillTimer = _bombSkillCoolTime;
            Instantiate(BombSkillPrefab);
        }
    }
}
