using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : EnemyBase
{
	[SerializeField] public int BaseHealth = 15;
	[SerializeField] public float SpellCastInterval = 13f;
    private float LastSpellCastTimer = 1f;

	// Start is called before the first frame update
	void Start()
    {
		MaxHealth = CalculateMaxHealth(BaseHealth);
		DisplayName = "Wizard";
	}

    // Update is called once per frame
    void Update()
    {
		LastSpellCastTimer += Time.deltaTime;
		if (LastSpellCastTimer >= SpellCastInterval)
		{
			Events.OnTimeStop();
			LastSpellCastTimer = 0f;
		}
	}
}
