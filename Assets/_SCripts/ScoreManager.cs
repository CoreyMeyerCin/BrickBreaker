using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event Action<int> OnPointsAdded = delegate { };

    private int score = 0;
    private Text scoreText;

    public int killcount = 0;

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
        //TODO: ScoreText
        scoreText = FindObjectOfType<Text>();
        scoreText.text = "Score: 0";
	}

	private void OnEnable()
	{
        EventManager.Instance.OnBlockDestroyed += OnBlockDestroyed();
	}

	private void OnBlockDestroyed()
    {

    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
        OnPointsAdded(score);
    }
    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}