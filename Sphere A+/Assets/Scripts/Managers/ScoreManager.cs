using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    // Retrieve score of current game.
    public int Score
    {
        get; private set;
    } // End Score.

    // Action event.
    public static event Action<int> OnScoreAdded;

    private void OnEnable()
    {
        Platform.OnCollideWithPlayer += HandleOnCollideWithPlayer;
    } // End OnEnable.

    private void OnDisable()
    {
        Platform.OnCollideWithPlayer -= HandleOnCollideWithPlayer;
    } // End OnDisable.

    // Update score.
    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
        OnScoreAdded?.Invoke(Score);
    } // End AddScore.

    // Update and retrieve best score of the level.
    public int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Using PlayerPrefs to retrieve and update best score.
        if (Score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", Score);
            return Score;
        } // End if.
        else
        {
            return bestScore;
        } // End else.
    } // End GetBestScore.

    // Add +1 score.
    void HandleOnCollideWithPlayer()
    {
        AddScore(1);
    } // End HandleOnCollideWithPlayer.
} // End script.