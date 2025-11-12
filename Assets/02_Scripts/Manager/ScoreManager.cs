using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    [SerializeField] private Text _currentScoreTextUI;
    private int _currentScore = 0;

    private void Start()
    {
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;
        _currentScore += score;
        Refresh();
    }

    private void Refresh()
    { 
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore}";
    }
}
