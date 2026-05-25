using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private BallController ball;
    [SerializeField] private Transform ballStartPosition;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int winningScore = 5;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private AIMovement aiMovement;

    [Header("UI Objects")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private int _playerScore;
    private int _aiScore;
    private bool _gameOver;

    public static bool SkipMenu { get; set; } = false; // Static property to determine whether to skip the menu

    private void Start()
    {
        if (SkipMenu)
        {
            SkipMenu = false; // Reset SkipMenu to false for the next time the game starts
            StartGame(); // If SkipMenu is true, start the game immediately
            return;
        }

        Time.timeScale = 0; // Pause the game by setting time scale to 0
        menuPanel.SetActive(true); // Show the menu panel
        scorePanel.SetActive(false); // Hide the score panel
        gameOverPanel.SetActive(false); // Hide the game over panel
    }

    public void StartGame()
    {
        menuPanel.SetActive(false); // Hide the menu panel
        scorePanel.SetActive(true); // Show the score panel

        Time.timeScale = 1f; // Resume the game by setting time scale to 1
    }

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
