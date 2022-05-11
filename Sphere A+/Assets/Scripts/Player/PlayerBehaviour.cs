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

        _playerMovement.Jump();

        SoundController.Instance.PlayAudio(AudioType.JUMP);
    } // End OnCollisionEnter.

    public void OnGameOver()
    {
        gameObject.SetActive(false);
    } // End OnGameOver.

    internal void Revive()
    {
        _triggerFirstMove = false;

        _playerInput.ResetFirstMove();

        _playerMovement.Revive(lastCollidePosition);

        gameObject.SetActive(true);
    } // End Revive.
} // End script.
