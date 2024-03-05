using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

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
    }

    void Start()
    {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = "Score: 0 | KC: 0";
	}

    private void OnEnable()
    {
        Events.OnBlockDestroyed += BlockDestroyed;
        Events.OnPointsAdded += UpdateScoreText;
    }

	private void OnDisable()
	{
		Events.OnBlockDestroyed -= BlockDestroyed;
		Events.OnPointsAdded -= UpdateScoreText;
	}

	private void BlockDestroyed(Block block)
    {
        killcount++;
        AddPoints(block.points);
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Score: {score} | KC: {killcount}";
        Events.OnPointsAdded(score);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score} | KC: {killcount}";
    }
}