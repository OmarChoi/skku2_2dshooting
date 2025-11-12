using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    [SerializeField] private Text _currentScoreTextUI;
    private int _currentScore = 0;
    private const string ScoreKey = "Score";

    private void Start()
    {
        LoadScore();
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;
        _currentScore += score;
        Refresh();
        SaveScore();
    }

    private void Refresh()
    { 
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, _currentScore);
    }

    private void LoadScore()
    {
        _currentScore = PlayerPrefs.GetInt(ScoreKey, 0);
    }
}
