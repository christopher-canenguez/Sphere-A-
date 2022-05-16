using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour
{
    PlayerMovement _playerMovement;
    PlayerInput _playerInput;

    bool _triggerFirstMove = false;

    Vector3 lastCollidePosition;

    public event Action OnFirstJump;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    } // End awake.

    // This will be what is called when the game starts, where the player is waiting on the platform for movement to start.
    public void FirstJump()
    {
        if (_triggerFirstMove)
        {
            return;
        } // End if.

        _triggerFirstMove = true;

        OnFirstJump?.Invoke();

        _playerMovement.Jump();
    } // End FirstJump.

    // Jump whenever the player comes into contact with the platform.
    private void OnCollisionEnter(Collision collision)
    {
        if (!_triggerFirstMove)
        {
            return;
        } // End if.

        Vector3 contactNormal = collision.GetContact(0).normal;

        if (Mathf.Abs(contactNormal.x) == 1f)
        {
            return;
        } // End if.


        lastCollidePosition = transform.position;

        // Jump.
        _playerMovement.Jump();

        // Play jump sound whenever the player comes into contact with the platform object.
        SoundController.Instance.PlayAudio(AudioType.JUMP);
    } // End OnCollisionEnter.
} // End script.
