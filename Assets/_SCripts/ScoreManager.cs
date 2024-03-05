using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Text ScoreText;
    public Text BlockText;

    public event Action OnPointsAdded = delegate { };
    
    public delegate void LevelUpAction();
    public static event LevelUpAction OnLevelUp;

    private int score = 0;
    public int CorruptedBlocks = 0;


    void OnEnable()
    {
        ScoreManager.OnLevelUp += LevelUp;
    }

    void OnDisable()
    {
        ScoreManager.OnLevelUp -= LevelUp;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        OnPointsAdded += UpdateScoreText;
    }

    void Start()
    {
        ScoreText.text = "Score: 0";
        BlockText.text = "Corruption: 0";
    }

    public void AddPoints(int points)
    {
        score += points;
        OnPointsAdded();
        if (score >= 500)
        {
            OnLevelUp?.Invoke();
        }
    }
        public void UpdateScoreText()
    {
        ScoreText.text = $"Score: {score}";
        BlockText.text = $"Corruption: {CorruptedBlocks}";
    }

    void LevelUp()
    {
        Time.timeScale = 0f;
    }
    
}