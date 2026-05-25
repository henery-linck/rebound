using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float speedIncreaseFactor = 0.2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float hitOffsetModifier = 1.0f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float _lastHitTime;
    private float _initialSpeed = 5f;
    private TrailRenderer _trail;

    public TrailRenderer Trail => _trail;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(-1, Random.Range(-0.7f, 0.7f)).normalized;
        _initialSpeed = speed;
        _trail = GetComponent<TrailRenderer>();
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
            hitOffset *= hitOffsetModifier;

            direction = new Vector2(
                -direction.x,
                direction.y + hitOffset * 0.5f
            ).normalized;

            speed = Mathf.Min(speed + speedIncreaseFactor, maxSpeed);
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
        speed = _initialSpeed;
    }

    internal void StopBall()
    {
        direction = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }
}