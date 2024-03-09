using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : EnemyBase
{
	[SerializeField]  public int BaseHealth = 25;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = CalculateMaxHealth(BaseHealth);
        DisplayName = "Goblin";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
