using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        direction = new Vector2(-1, Random.Range(-0.7f, 0.7f)).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            float hitOffset = transform.position.y - collision.transform.position.y;

            direction = new Vector2(
                -direction.x,
                direction.y + hitOffset * 0.5f
            ).normalized;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = new Vector2(direction.x, -direction.y).normalized;
        }
    }

    public void Reset(int directionX)
    {
        direction = new Vector2(directionX, Random.Range(-0.7f, 0.7f)).normalized;
    }

    internal void StopBall()
    {
        direction = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }
}