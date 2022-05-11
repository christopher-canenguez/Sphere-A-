using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public int Score
    {
        get; private set;
    } // End Score.

    public static event Action<int> OnScoreAdded;

    private void OnEnable()
    {
        Platform.OnCollideWithPlayer += HandleOnCollideWithPlayer;
    } // End OnEnable.

    private void OnDisable()
    {
        Platform.OnCollideWithPlayer -= HandleOnCollideWithPlayer;
    } // End OnDisable.

    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
        OnScoreAdded?.Invoke(Score);
    } // End AddScore.

    public int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

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

    void HandleOnCollideWithPlayer()
    {
        AddScore(1);
    } // End HandleOnCollideWithPlayer.
} // End script.
