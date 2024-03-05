using UnityEngine;

public enum EdgeType { Horizontal, Vertical, Variable, Death }

public class Solid : MonoBehaviour
{
    public EdgeType edgeType;

    void Start()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(transform.localScale.x, transform.localScale.y);
    }
}