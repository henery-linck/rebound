using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for score
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component for score sound
    [SerializeField] private AudioClip scoreSound; // Reference to the AudioClip for scoring

    public void UpdateScore(int playerScore, int aiScore)
    {
        scoreText.text = $"{playerScore} - {aiScore}";

        if (scoreSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }
}
