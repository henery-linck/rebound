using System.Threading;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private float speed = 4f; // Speed of the AI movement
    [SerializeField] private float positionOffset = 0.2f; // Offset to prevent jittery movement
    [SerializeField] private float reactionTime = 0.3f; // Time delay before the AI reacts to the ball's movement
    [SerializeField] private float errorMargin = 0.5f;

    private Rigidbody2D _rigidBody;
    Vector2 _ballDirection;
    private float _targetY;
    private float _reactionTimer;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _reactionTimer += Time.deltaTime;

        if (_reactionTimer >= reactionTime)
        {
            if (ball.linearVelocity.x > 0) // Only react if the ball is moving towards the AI
            {
                _targetY = ball.position.y + Random.Range(-errorMargin, errorMargin); // Add some randomness to the target position
            }
            else
            {
                _targetY = Mathf.Lerp(_targetY, 0, 0.05f); // Reset target position when the ball is moving away
            }

            _reactionTimer = 0f; // Reset the reaction timer
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _ballDirection = ball.linearVelocity.normalized;
        float offset = _targetY - transform.position.y;

        if (Mathf.Abs(offset) > positionOffset)
        {
            float direction = Mathf.Clamp(offset, -1f, 1f);
            _rigidBody.linearVelocity = new Vector2(0, direction * speed);
        }
        else
        {
            _rigidBody.linearVelocity = Vector2.zero;
        }
    }
}
