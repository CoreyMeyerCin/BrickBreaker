using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Text ScoreText;
    public Text BlockText;

    private int Score = 0;
	public int killcount = 0;
	public int CorruptedBlocks = 0;


	private void OnEnable()
	{
		Events.OnBlockDestroyed += BlockDestroyed;
        Events.OnLeveLUp += LevelUp;
	}

	private void OnDisable()
	{
		Events.OnBlockDestroyed -= BlockDestroyed;
		Events.OnLeveLUp -= LevelUp;
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
    }

    void Start()
    {
        ScoreText = FindObjectOfType<Text>();
		ScoreText.text = "Score: 0";
		BlockText.text = "Corruption: 0";
	}

	private void BlockDestroyed(Block block)
	{
		killcount++;
		AddPoints(block.points);
	}

	public void AddPoints(int points)
    {
        Score += points;
        UpdateScoreText();
        if (Score >= 500)
        {
            Events.OnLeveLUp();
        }
    }

    public void UpdateScoreText()
    {
        ScoreText.text = $"Score: {Score}";
        BlockText.text = $"Corruption: {CorruptedBlocks}";
    }

    void LevelUp()
    {
        Time.timeScale = 0f;
    }
    
    
}