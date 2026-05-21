using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float _lastHitTime;

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

        if (Time.time - _lastHitTime > 0.05f)
        {
            _lastHitTime = Time.time;
            PlayHitSound();
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            float volume = rb.linearVelocity.magnitude / maxSpeed;
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(hitSound);
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