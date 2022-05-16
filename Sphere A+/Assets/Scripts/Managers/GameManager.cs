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

    ScoreManager _scoreManager;

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame;

    // Upon the start of the game, these will occur.
    private void Awake()
    {
        // Get ScoreManager.
        _scoreManager = GetComponent<ScoreManager>();

        // Player will wait on start platform until movement using the slider is committed.
        _player.OnFirstJump += StartGame;

        // Activate and deactivate panels for the start of level.
        _startPanel.SetActive(true);
        _gamePlayPanel.SetActive(false);
        _gameOverPanel.SetActive(false);

        // Get the high score the owner of device has gotten to with this game.
        highScore = PlayerPrefs.GetInt("Level 01", 0);

    } // End Awake.

    void StartGame()
    {
        // Player will start jumping.
        OnStartGame?.Invoke();

        // Deactivate menu panel and start gameplay menu.
        _startPanel.SetActive(false);
        _gamePlayPanel.SetActive(true);
    } // End StartGame.

    public void EndGame()
    {
        if (_isGameOver)
        {
            return;
        } // End if.

        // When game ends (player dies):
        // Player deactivates.
        // No more platforms spawned.
        // UI slider deaactivates.
        // gameplay panel off, gameover panel on.
        _isGameOver = true;
        //_player.OnGameOver();
        _spawner.OnGameOver();
        _UISlider.SetActive(false);
        _gamePlayPanel.SetActive(false);
        _gameOverPanel.SetActive(true);

        // Retrieve score from current game, and last best score.
        _bestScoreText.text = "Best: " + _scoreManager.Score.ToString();
        _lastScoreText.text = "Last: " + _scoreManager.GetBestScore();

        //SoundController.Instance.PlayAudio(AudioType.GAMEOVER);
    } // End EndGame.

    private void FixedUpdate()
    {
        // Update score as game goes on.
        _currentScoreText.text = _scoreManager.Score.ToString();
    } // End FixedUpdate.
} // End script.
