using System.Drawing;
using UnityEngine;

public class PlayerBlock : Solid
{
    public float Speed = 1f;
    public Rigidbody2D Rb;
	private Vector3 size;
	private float increaseScaleByAmount = 0.5f;
	private float decreaseScaleByAmount = 0.5f;

	void Start()
    {
        Rb = gameObject.AddComponent<Rigidbody2D>(); 
        Rb.gravityScale = 0;
        Rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        this.edgeType = EdgeType.Variable;
		size = gameObject.transform.localScale;
	}

	private void OnEnable()
	{
		Events.OnPaddleExpand += PaddleExpand;
        Events.OnPaddleDecrease += PaddleDecrease;
	}

	private void OnDisable()
	{
        Events.OnPaddleExpand -= PaddleExpand;
		Events.OnPaddleDecrease -= PaddleDecrease;
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

    private void PaddleExpand()
    {
		AdjustSize(new Vector3(increaseScaleByAmount, 0, 0));
	}

    private void PaddleDecrease()
    {
        AdjustSize(new Vector3(-decreaseScaleByAmount, 0, 0));
    }

	private void AdjustSize(Vector3 amountToAdjustVector)
	{
		size += amountToAdjustVector;
		gameObject.transform.localScale = size;
	}
}