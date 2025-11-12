using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _highScoreTextUI;
    private int _currentScore = 0;
    private int _highScore = 0;
    private const string HighScoreKey = "HighScore";
    
    private Tweener _shakeTween;
    private float _shakeDuration = 0.1f;
    private float _shakeSize = 1.25f;
    private void Start()
    {
        LoadHighScore();
        Refresh();


        _shakeTween = _currentScoreTextUI.transform
            .DOShakeScale(_shakeDuration, _shakeSize)
            .SetAutoKill(false)
            .Pause();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;
        _currentScore += score;
        ShakeScoreUI();
        Refresh();

        if (_currentScore < _highScore) return;
        _highScore = _currentScore;
        RefreshHighScoreText();
    }

    private void ShakeScoreUI()
    {
        _shakeTween.Restart();
    }

    private void Refresh()
    { 
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
    }

    private void RefreshHighScoreText()
    {
        _highScoreTextUI.text = $"최고 점수 : {_highScore:N0}";
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, _highScore);
    }

    private void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        RefreshHighScoreText();
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
