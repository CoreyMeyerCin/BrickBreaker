using UnityEngine;

public class PlayerBlock : Solid
{
    public float Speed = 1f;
    public Rigidbody2D Rb;

    void Start()
    {
        Rb = gameObject.AddComponent<Rigidbody2D>(); 
        Rb.gravityScale = 0;
        Rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        this.edgeType = EdgeType.Vertical;
    }

    void Update()
    {
        float xPos = transform.position.x;
        Vector2 velocity = Rb.velocity;

        if (Input.GetKey(KeyCode.A) && xPos > -7.6f)
        {
            velocity.x = -10 * Speed;
        }
        else if (Input.GetKey(KeyCode.D) && xPos < 7.6f)
        {
            velocity.x = 10 * Speed;
        }
        else
        {
            velocity.x = 0;
        }

        Rb.velocity = velocity;
    }
}