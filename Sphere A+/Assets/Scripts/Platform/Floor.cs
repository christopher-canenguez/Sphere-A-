using UnityEngine;
using UnityEngine.Events;

public class Floor : MonoBehaviour
{
    public UnityEvent OnGameOver;

    public AudioSource _gameOverSound;

    private void Start()
    {
        _gameOverSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnGameOver?.Invoke();
            _gameOverSound.Play();
        } // End if.
    } // End method.
} // End class.
