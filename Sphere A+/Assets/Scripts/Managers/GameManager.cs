using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] PlatformSpawner _spawner;

    [SerializeField] GameObject _UISlider;

    [SerializeField] public GameObject _startPanel;

    [SerializeField] public GameObject _gamePlayPanel;
    [SerializeField] TMP_Text _currentScoreText;

    [SerializeField] public GameObject _gameOverPanel;
    [SerializeField] TMP_Text _bestScoreText;
    [SerializeField] TMP_Text _lastScoreText;

    [SerializeField] AudioClip _gameOverSound;

    // Will be used to keep high score.
    int score;
    public static int highScore;

    public static int _gameplayCount;

    bool _isGameOver = false;
    bool _isRevive = false;

    ScoreManager _scoreManager;

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
        _player.OnFirstJump += StartGame;
        _startPanel.SetActive(true);
        _gamePlayPanel.SetActive(false);
        _gameOverPanel.SetActive(false);

        highScore = PlayerPrefs.GetInt("Level 01", 0);

    } // End Awake.

    void StartGame()
    {
        OnStartGame?.Invoke();
        _startPanel.SetActive(false);
        _gamePlayPanel.SetActive(true);
    } // End StartGame.

    public void EndGame()
    {
        if (_isGameOver)
        {
            return;
        } // End if.

        _isGameOver = true;
        _player.OnGameOver();
        _spawner.OnGameOver();
        _UISlider.SetActive(false);
        _gamePlayPanel.SetActive(false);
        _gameOverPanel.SetActive(true);

        _bestScoreText.text = "Best: " + _scoreManager.Score.ToString();
        _lastScoreText.text = "Last: " + _scoreManager.GetBestScore();

        SoundController.Instance.PlayAudio(AudioType.GAMEOVER);
    } // End EndGame.

    private void FixedUpdate()
    {
        _currentScoreText.text = _scoreManager.Score.ToString();
    } // End FixedUpdate.
}
