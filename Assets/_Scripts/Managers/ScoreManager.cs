using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Manager<ScoreManager>
{
    public Text ScoreText;
    public Text BlockText;
    private int Score = 0;
    public int Level;
	public int Killcount = 0;
	public int CorruptedBlocks = 0;
    public int CurrentStage = 1;
    private int requiredScore;
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
        requiredScore = 100;
        Level = 1;
	}

	private void BlockDestroyed(Block block)
	{
		Killcount++;
		AddPoints(block.points);

        /*I will keep this commented incase we want to introduce some mechanic that levels up the player after a certain number of kills
            But this was double triggering level up in the same fram as AddPoints    
        */
        // if (Killcount > 1 && Killcount % 5 == 0)
        // {
        //     Events.OnLeveLUp();
        // }
	}

	public void AddPoints(int points)
    {
        Score += points;
        UpdateScoreText();
        if (Score >= requiredScore)
        {
            Debug.Log("Addpoints Score:" + Score);
            Debug.Log("Addpoints RequriedScore:" + requiredScore);
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
        UIManager.Instance.ShowSkillCanvas();
        Level++;
        Score -= requiredScore;
        requiredScore = 100 + Level * 50;
        MenuManager.Instance.LevelUp();
    }
}