using UnityEngine;
using System.Collections.Generic;

public enum EPatternType
{
    Directional,
    Curve,
}

public enum EBossState
{
    Idle,
    Attack,
}

public class BossAttack : MonoBehaviour
{
    [Header("보스 상태")]
    private EBossState _currentState = EBossState.Idle;
    private float _stateTimer = 0.0f;
    private float _idleDuration = 3.0f;

    [Header("탄막 패턴")]
    private EPatternType _currentPattern = EPatternType.Directional;
    private Dictionary<EPatternType, IBulletPattern> _availablePatterns = new Dictionary<EPatternType, IBulletPattern>();
    private int[] _typeWeights = { 100 };
    private int _totalWeight;

    [Header("패턴 타이머")]
    private float _lastPatternTime = 0;
    private float _patternTimer = 0;
    private float _patternDuration = 8.0f;
    private float _patternInterval= 1.0f;
    
    
    private void Awake()
    {
        for(int i = 0; i < _typeWeights.Length; ++i)
        {
            _totalWeight += _typeWeights[i];
        }
        RegistPatternType();
    }

    private void RegistPatternType()
    {
        _availablePatterns.Add(EPatternType.Directional, new DirectionalBulletPatern());
    }

    private void Update()
    {
        if (_currentState == EBossState.Idle)
        {
            _stateTimer += Time.deltaTime;
            if (_stateTimer > _idleDuration)
            {
                _stateTimer = 0.0f;
                _currentState = EBossState.Attack;
            }
            return;
        }

        ExecuteBullet();
        ChoosePattern();
    }

    private void ExecuteBullet()
    {
        float currentTime = Time.time;
        float durationTime = currentTime - _lastPatternTime;
        if (durationTime > _patternInterval)
        {
            _lastPatternTime = currentTime;
            _availablePatterns[_currentPattern].ExecuteBullet(transform.position);
        }
    }

    private void ChoosePattern()
    {
        _patternTimer += Time.deltaTime;
        if (_patternTimer < _patternDuration) return;
        _currentState = EBossState.Idle;
        _patternTimer = 0;
        int next = Utils.GetRandomIndexByWeight(_totalWeight, _typeWeights);
        _currentPattern = (EPatternType)next;
    }
}
