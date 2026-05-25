using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [SerializeField] private Transform ballStartPosition;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int winningScore = 5;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TextMeshProUGUI gameOverText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private AIMovement aiMovement;

    private int _playerScore;
    private int _aiScore;
    private bool _gameOver;

    private void Update()
    {
        if (_gameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        aiMovement.ResetErrorMargin();
        _playerScore = 0;
        _aiScore = 0;
        _gameOver = false;
        scoreManager.UpdateScore(_playerScore, _aiScore);
        gameOverPanel.SetActive(false);

        ResetBall(Random.value > 0.5f ? 1 : -1);
    }

    public void ScorePlayer()
    {
        if (_gameOver) return;

        _playerScore++;
        CheckGameOver();
        scoreManager.UpdateScore(_playerScore, _aiScore);
        aiMovement.DecreaseErrorMargin();

        if (!_gameOver)
            ResetBall(1);
    }

    private void CheckGameOver()
    {
        if (_playerScore >= winningScore)
        {
            audioSource.PlayOneShot(victorySound);
            EndGame("YOU WIN!");
        }
        else if (_aiScore >= winningScore)
        {
            audioSource.PlayOneShot(defeatSound);
            EndGame("YOU LOSE!");
        }
    }

    private void EndGame(string text)
    {
        _gameOver = true;

        gameOverPanel.SetActive(true);
        gameOverText.text = text;

        ball.StopBall();
    }

    public void ScoreAI()
    {
        if (_gameOver) return;

        _aiScore++;
        CheckGameOver();
        scoreManager.UpdateScore(_playerScore, _aiScore);

        if (!_gameOver)
            ResetBall(-1);
    }

    private void ResetBall(int direction)
    {
        ball.transform.position = ballStartPosition.position;
        Debug.Log($"Player Score: {_playerScore} | AI Score: {_aiScore}");
        ball.Reset(direction);
    }
}
