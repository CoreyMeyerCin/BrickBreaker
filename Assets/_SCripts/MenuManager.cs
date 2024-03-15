using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance { get; private set; }

	public GameObject SelectPowerupPanel;
    public GameObject SelectPowerupMainPanel;
	private List<GameObject> SelectPowerupButtons;

	private readonly int NumberOfAvailablePowerups = 2;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnEnable()
	{
		Events.OnLeveLUp += LevelUp;
	}

	private void OnDisable()
	{
		Events.OnLeveLUp -= LevelUp;
	}

	// Start is called before the first frame update
	void Start()
    {
		SelectPowerupButtons = GameObject.FindGameObjectsWithTag("powerup_button")?.ToList();
		SelectPowerupPanel.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

	public void LevelUp()
	{
		Debug.Log("Leveling up");
		Time.timeScale = 0f;
		OpenMenu(SelectPowerupPanel);

		if (NumberOfAvailablePowerups < SelectPowerupButtons.Count)
		{
			var availableIndexesToHide = new List<int>();
			for (int i = 0; i < SelectPowerupButtons.Count; i++)
			{
				availableIndexesToHide.Add(i);
			}

			var buttonIndexesToHide = new List<int>();
			for (int i = 0; i < NumberOfAvailablePowerups; i++)
			{
				var buttonIdxToHide = availableIndexesToHide[Random.Range(0, availableIndexesToHide.Count)];
				buttonIndexesToHide.Add(buttonIdxToHide);
				availableIndexesToHide.Remove(buttonIdxToHide);
			}

			foreach (var idx in buttonIndexesToHide)
			{
				SelectPowerupButtons[idx].SetActive(false);

			}
		}
	}

	private void PowerupSelected()
	{
		ResetPowerupButtons();
		CloseMenu(SelectPowerupPanel);
		Time.timeScale = 1f;
	}

	private void ResetPowerupButtons()
	{
		var inactiveButtons = SelectPowerupButtons.Select(x => x).Where(x => !x.activeInHierarchy).ToList();
		if (inactiveButtons.Any())
		{
			foreach (var button in inactiveButtons)
			{
				button.SetActive(true);
			}
		}
	}

	public void PowerupExpandPaddle()
    {
        Events.OnPowerupPaddleExpand();
		PowerupSelected();
	}

    public void PowerupIncreasePower()
    {
        Events.OnPowerupPowerIncrease();
		PowerupSelected();
	}

    public void PowerupIncreaseBallSpeed()
    {
        Events.OnPowerupBallSpeedIncrease();
		PowerupSelected();
	}

	public void PowerupIncreaseBallSize()
	{
		Events.OnPowerupBallSizeIncrease();
		PowerupSelected();
	}
}
