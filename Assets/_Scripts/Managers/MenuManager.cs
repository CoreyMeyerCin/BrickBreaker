using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : Manager<MenuManager>
{
	public GameObject SelectPowerupPanel;
    public GameObject SelectPowerupMainPanel;
	private List<GameObject> SelectPowerupButtons;

	private readonly int NumberOfAvailablePowerups = 2;

	private void OnEnable()
	{
		Events.OnLeveLUp += LevelUp;
	}

	private void OnDisable()
	{
		Events.OnLeveLUp -= LevelUp;
	}

	void Start()
    {
		SelectPowerupButtons = GameObject.FindGameObjectsWithTag("powerup_button")?.ToList();
		SelectPowerupPanel = GameObject.Find("SelectPowerupPanel");
    	SelectPowerupPanel.SetActive(false);
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
		//Debug.Log("Leveling up");
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
			if (SelectPowerupPanel != null)
    		{
    		    SelectPowerupPanel.SetActive(true);
    		}
    		else
    		{
    		    //Debug.LogError("No GameObject with the tag 'select_powerup_panel' was found.");
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