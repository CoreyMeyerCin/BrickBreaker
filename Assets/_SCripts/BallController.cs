using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Vector2 direction;

void Start()
{
    rb = gameObject.AddComponent<Rigidbody2D>();
    rb.gravityScale = 0;
    StartCoroutine(RespawnBall());
    rb.velocity = direction * speed;
    rb.freezeRotation = true;
    rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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
                        Debug.Log("Hit horizontal edge");
                        break;
                    case EdgeType.Vertical:
                        direction.y *= -1;
                        Debug.Log("Hit vertical edge");
                        break;
                    case EdgeType.Death:
                        StartCoroutine(RespawnBall());
                        Debug.Log("Hit death tag");
                        break;
                    case EdgeType.Variable:
                        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
                            Debug.Log("A and D key are being pressed.");
                        }
                        else if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                        {
                            Debug.Log("A key is being pressed.");
                            direction.x += speed * -0.1f;
                        }
                        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                        {
                            Debug.Log("D key is being pressed.");
                            direction.x += speed * 0.1f;
                        }
                        direction.y *= -1;
                        Debug.Log("Hit player tag");
                        break;
                    }
                
                rb.velocity = direction * speed;
            }

            
            try
            {
                Block block = collision.gameObject.transform.parent.GetComponent<Block>();
                if (block != null)
                {
                    Debug.Log("Hit Block:" + block.name.ToString());
                    block.HitBlock();
                }
            }
            catch (System.Exception e)
            {
                //Debug.Log("An error occurred: " + e.Message);
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