using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public string DisplayName { get; set; }
    public int CurrentHealth { get; set; } 
    public int MaxHealth { get; set; }

	private void OnEnable()
	{
		Events.OnBlockDestroyed += TakeDamage;
	}

	private void OnDisable()
	{
		Events.OnBlockDestroyed -= TakeDamage;
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal int CalculateMaxHealth(int baseHealth)
    {
        return baseHealth * ScoreManager.Instance.CurrentStage;
    }

    private void TakeDamage(Block block)
    {
        CurrentHealth--;
    }
}
