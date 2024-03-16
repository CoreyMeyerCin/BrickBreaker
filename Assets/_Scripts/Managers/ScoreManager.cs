using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Manager<ScoreManager>
{
    public Text ScoreText;
    public Text BlockText;
    private int Score = 0;
    public int Level = 1;
	public int Killcount = 0;
	public int CorruptedBlocks = 0;
    public int CurrentStage = 1;
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

    void Start()
    {
		ScoreText.text = "Score: 0";
		BlockText.text = "Corruption: 0";
	}

	private void BlockDestroyed(Block block)
	{
		Killcount++;
		AddPoints(block.points);

        if (Killcount > 1 && Killcount % 5 == 0)
        {
            Events.OnLeveLUp();
        }
	}

	public void AddPoints(int points)
    {
        Score += points;
        UpdateScoreText();
        if (Score >= 100)
        {
            Events.OnLeveLUp();
        }
    }

    public void UpdateScoreText()
    {
        ScoreText.text = $"Score: {Score}";
        BlockText.text = $"Corruption: {CorruptedBlocks}";
    }

    private void LevelUp()
    {
        Time.timeScale = 0f;
        Debug.Log("Level Up");
        UIManager.Instance.ShowSkillCanvas();
        Level++;
        MenuManager.Instance.LevelUp();
    }
}