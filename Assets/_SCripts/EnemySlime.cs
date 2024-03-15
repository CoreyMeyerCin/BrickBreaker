using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyBase
{
	[SerializeField] public int BaseHealth = 30;

	// Start is called before the first frame update
	void Start()
    {
		MaxHealth = CalculateMaxHealth(BaseHealth);
		DisplayName = "Slime";
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
