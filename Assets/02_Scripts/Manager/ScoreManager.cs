using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // 현재 점수 UI
    // 현재 점수
    public Text CurrentScoreTextUI;
    private int _currentScore = 0;

    private void Start()
    {
        CurrentScoreTextUI.text = $"현재 점수 : {_currentScore}";
    }

    
}
