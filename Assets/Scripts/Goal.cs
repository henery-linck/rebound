using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isPlayerGoal;
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (isPlayerGoal)
            {
                gameManager.ScoreAI();
            }
            else
            {
                gameManager.ScorePlayer();
            }
        }
    }
}
