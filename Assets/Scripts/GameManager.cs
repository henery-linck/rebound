using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [SerializeField] private Transform ballStartPosition;
    [SerializeField] private ScoreManager scoreManager;

    private int _playerScore;
    private int _aiScore;

    public void ScorePlayer()
    { 
        _playerScore++;
        scoreManager.UpdateScore(_playerScore, _aiScore);
        ResetBall(1);
    }

    public void ScoreAI()
    {
        _aiScore++;
        scoreManager.UpdateScore(_playerScore, _aiScore);
        ResetBall(-1);
    }

    private void ResetBall(int direction)
    {
        ball.transform.position = ballStartPosition.position;
        Debug.Log($"Player Score: {_playerScore} | AI Score: {_aiScore}");
        ball.Reset(direction);
    }
}
