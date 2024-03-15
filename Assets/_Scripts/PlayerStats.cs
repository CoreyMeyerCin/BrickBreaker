using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public int Health { get; set; }
    public float BallSpeedModifier {get;set;} =1f;

    public PlayerStats(int health, float ballSpeedModifier)
    {
        Health = health;
        BallSpeedModifier = ballSpeedModifier;
    }
    public void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}