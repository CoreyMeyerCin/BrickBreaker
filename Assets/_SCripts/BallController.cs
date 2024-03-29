using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float speed;
    public int damage = 0;
    private int baseDamage = 1;
    private int addedDamage = 0;
    public int incrementPowerPerXKills; //this doesn't want to accept a default value set here? Always ends up as 1. Set it in start as a workaround
	private Vector2 size;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Vector2 previousPosition = new Vector2(0, 0);
    private const float positionThreshold = 0.01f;
    public SoundTrigger SoundTrigger;

    public float lastHitTimer = 1f;
    private float ballActiveTimer = 1f;
    private bool isTimeStop = false;
    private Vector2 direction;
    public float increaseScaleByAmount = 0.1f;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(RespawnBall());
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = direction * baseSpeed;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        size = gameObject.transform.localScale;

        speed = baseSpeed;
        incrementPowerPerXKills = 5;
        SoundTrigger = gameObject.GetComponent<SoundTrigger>();
    }

	private void OnEnable()
	{
		Events.OnBlockDestroyed += BlockDestroyed;

        Events.OnPowerupPowerIncrease += PowerIncreased;
        Events.OnPowerupBallSpeedIncrease += SpeedIncreased;
        Events.OnPowerupBallSizeIncrease += SizeIncreased;

        Events.OnTimeStop += TimeStopWrapper;
	}

	private void OnDisable()
	{
		Events.OnBlockDestroyed -= BlockDestroyed;

		Events.OnPowerupPowerIncrease -= PowerIncreased;
		Events.OnPowerupBallSpeedIncrease -= SpeedIncreased;
		Events.OnPowerupBallSizeIncrease -= SizeIncreased;

		Events.OnTimeStop -= TimeStopWrapper;
	}

    void Update()
    {
        ballActiveTimer += Time.deltaTime;
		lastHitTimer += Time.deltaTime;
        if (!isTimeStop)
        {
            if (lastHitTimer >= 0.1f)
            {
                lastHitTimer = 0f;

                if (Vector2.Distance(gameObject.transform.position, previousPosition) < positionThreshold)
                {
                    // //Debug.Log("gameObject.position: " + gameObject.transform.position);
                    // //Debug.Log("previousPosition: " + previousPosition);
                    // //Debug.Log("Distance: " + Vector2.Distance(rb.position, previousPosition));

                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    rb.velocity = randomDirection * speed;
                }
                previousPosition = gameObject.transform.position;
            }

            if (rb.velocity.magnitude != speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }

            if (ballActiveTimer > 6)
            {
                //Events.OnTimeStop(); //########################### COMMENT OUT TO TURN OFF TIME STOP TESTING SPAM - I only left it in so you can get jumpscared by it 
                //lmao, I was so confused by what was going on XD
                
                ballActiveTimer = 0f;
            }
        }
    }


	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("solid"))
        {
            Solid solid = collision.gameObject.GetComponent<Solid>();

            if (solid != null)
            {
                switch (solid.edgeType)
                {
                    case EdgeType.Horizontal:
                        direction.x *= -1;
                        break;
                    case EdgeType.Vertical:
                        direction.y *= -1;
                        break;
                    case EdgeType.Death:
                        StartCoroutine(RespawnBall());
                        break;
                    case EdgeType.Variable:
                        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                        {
                        }
                        else if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                        {
                            direction.x += speed * -0.1f;
                        }
                        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                        {
                            direction.x += speed * 0.1f;
                        }
                        direction.y *= -1;
                        break;
                }
                
                rb.velocity = direction * speed;
            }

            try
            {
                Block block = collision.gameObject.transform.parent.GetComponent<Block>();
                if (block != null)
                {
                    SoundTrigger.PlaySound();
                    block.HitBlock(damage);
                }
            }
            catch (System.Exception e)
            {
            }
		}
	}

    IEnumerator RespawnBall()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("spawn");
        if (spawnPoint != null)
        {
            yield return new WaitForSeconds(0.02f); 

            speed = baseSpeed;
            transform.position = spawnPoint.transform.position;
            float randomX = Random.Range(-5f, 5f); 
            float randomY = Mathf.Abs(Random.Range(3f, 5f)); 
            direction = new Vector2(randomX, randomY).normalized;
            rb.velocity = direction * speed; 
        }
    }

    public void BlockDestroyed(Block block)
    {
        if (ScoreManager.Instance.Killcount > 0 && ScoreManager.Instance.Killcount % incrementPowerPerXKills == 0) 
        {
            UpdateDamage();
            AdjustSize(new Vector2(increaseScaleByAmount, increaseScaleByAmount));
        }
    }

	private void AdjustSize(Vector2 amountToAdjustVector)
	{
		//size = Vector2.Scale(size, new Vector2(increaseScaleByAmount, increaseScaleByAmount)); //other modifications keep making the ball disappear, don't have time to debug atm
		size += amountToAdjustVector;
        gameObject.transform.localScale = size;
	}

    private void UpdateDamage()
    {
        var killcount = ScoreManager.Instance.Killcount;
        addedDamage = Mathf.FloorToInt(killcount / incrementPowerPerXKills);
        damage = baseDamage + addedDamage;
    }

    private void PowerIncreased()
    {
        baseDamage += 1;
        UpdateDamage();
    }

    private void SpeedIncreased()
    {
        baseSpeed += 1;
        speed += 1;
    }

    private void SizeIncreased()
    {
		AdjustSize(new Vector2(increaseScaleByAmount, increaseScaleByAmount));
	}


	private void TimeStopWrapper()
    {
        //Debug.Log("Triggering Time Stop");
		StartCoroutine(TimeStop());
	}

    private IEnumerator TimeStop()
    {
        isTimeStop = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.Sleep();

        var img = GameObject.FindGameObjectWithTag("time_stop_visual")?.GetComponent<RawImage>();
        if (img != null)
        {
            img.enabled = true;
            img.color = new Color(1, 1, 1, 1);
            yield return StartCoroutine(FadeImage(img));
            img.enabled = false;
            //Debug.LogError("Time Stopping");
        }
        else
        {
            //Debug.LogError("No GameObject with the tag 'time_stop_visual' and a RawImage component was found.");
        }

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            yield return null; // Wait for the next frame
        }

        rb.WakeUp();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        speed = baseSpeed * 2;
        rb.velocity = randomDirection * baseSpeed;
        
        isTimeStop = false;
    }

    private IEnumerator FadeImage(RawImage image)
    {
        // loop over 1.5 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    public IEnumerator TimeStart()
    {
        isTimeStop = false;

        var img = GameObject.FindGameObjectWithTag("time_stop_visual")?.GetComponent<RawImage>();
        if (img != null)
        {
            img.enabled = true;
            // loop over 1.5 second forward
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            img.enabled = false;
        }
        else
        {
            //Debug.LogError("No GameObject with the tag 'time_stop_visual' and a RawImage component was found.");
        }

        rb.WakeUp();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        speed = baseSpeed;
        rb.velocity = randomDirection * baseSpeed;
    }

    private IEnumerator SpeedBoostAndDecay(float boostAmount)
    {
        speed = boostAmount;
		for (float i = boostAmount; i >= 0; i -= Time.deltaTime)
		{
            speed -= i;
			yield return null;
		}
	}
}