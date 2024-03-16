using UnityEngine;
public class InputManager : Manager<InputManager>
{
     public PlayerBlock playerBlock;
     void Start()
     {
        //there should never be more than 1 of these, so this way of assignment is fine
        playerBlock = FindObjectOfType<PlayerBlock>();
     }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ScoreManager.Instance.AddPoints(10);
        }

        float xPos = playerBlock.transform.position.x;
        Vector2 velocity = playerBlock.Rb.velocity;

        if (Input.GetKey(KeyCode.A) && xPos > -7.6f)
        {
            velocity.x = -10 * playerBlock.Speed;
        }
        else if (Input.GetKey(KeyCode.D) && xPos < 7.6f)
        {
            velocity.x = 10 * playerBlock.Speed;
        }
        else
        {
            velocity.x = 0;
        }

        playerBlock.Rb.velocity = velocity;
    }
}