using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _highScoreTextUI;
    private int _currentScore = 0;
    private int _highScore = 0;

    private ScoreData _scoreData = null;

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
        _scoreData.HighScore = _highScore;
        string jsonData = JsonUtility.ToJson(_scoreData);
        PlayerPrefs.SetString(HighScoreKey, jsonData);
    }

    private void LoadHighScore()
    {
        string jsonData = PlayerPrefs.GetString(HighScoreKey);
        _scoreData = JsonUtility.FromJson<ScoreData>(jsonData);
        if (_scoreData == null)
        {
            _scoreData = new ScoreData();
            return;
        }
        _highScore = _scoreData.HighScore;
        RefreshHighScoreText();
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
