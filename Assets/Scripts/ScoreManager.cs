using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for score

    public void UpdateScore(int playerScore, int aiScore)
    {
        scoreText.text = $"{playerScore} - {aiScore}";
    }
}
