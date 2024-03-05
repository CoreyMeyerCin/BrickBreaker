using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Vector2 direction;
    private Vector2 previousPosition = new Vector2(0, 0);
    private const float positionThreshold = 0.01f;


    public float timer = 1f;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(RespawnBall());
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = direction * speed;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.1f){
            timer = 0f;

            if (Vector2.Distance(gameObject.transform.position, previousPosition) < positionThreshold)
            {
                Debug.Log("gameObject.position: " + gameObject.transform.position);
                Debug.Log("previousPosition: " + previousPosition);
                Debug.Log("Distance: " + Vector2.Distance(rb.position, previousPosition));

                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.velocity = randomDirection * speed;
            }
            previousPosition = gameObject.transform.position;
        } 

        if (rb.velocity.magnitude != speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
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
                        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
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
                    block.HitBlock();
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

            transform.position = spawnPoint.transform.position;
            float randomX = Random.Range(-5f, 5f); 
            float randomY = Mathf.Abs(Random.Range(3f, 5f)); 
            direction = new Vector2(randomX, randomY).normalized;
            rb.velocity = direction * speed; 
        }
    }
}