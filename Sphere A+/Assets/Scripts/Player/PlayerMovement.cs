using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _jumpheight = 5f;
    [SerializeField] float _jumpTime = 1f;
    [SerializeField] float _jumpDistance = 8f;
    [SerializeField] float _maxPosX = 3f;

    [Space]
    [Range(0, 1)] [SerializeField] float _smoothHorizontalTime = 0.2f;

    float _xVelocity;
    float _yVelocity;
    float _yGravity;

    float _elapsedTime = 0;
    float _startVal;
    float _endVal;

    Vector3 _playerPos;

    Rigidbody _rb;

    float GetJumpHalfTime => _jumpTime * .5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    } // End Awake.

    private void OnEnable()
    {
        PlayerInput.OnPointerDrag += HorizontalMovement;
    } // End OnEnable.

    private void OnDisable()
    {
        PlayerInput.OnPointerDrag -= HorizontalMovement;
    } // End OnDisable.

    private void Start()
    {
        // Calculate the vertical force.
        _yVelocity = (_jumpheight / GetJumpHalfTime) * 2;

        // Calculate and apply the gravity.
        _yGravity = -_yVelocity / GetJumpHalfTime;
        Physics.gravity = Vector3.up * _yGravity;
    } // End Start.

    private void Update()
    {
        // Calculaate the percentage.
        _elapsedTime += Time.deltaTime;

        float timePercentage = _elapsedTime / _jumpTime;

        Move(timePercentage);
    } // End Update.

    public void Jump()
    {
        _elapsedTime = 0;

        _startVal = transform.position.z;
        _endVal += _jumpDistance;

        // Apply the jump movement.
        _rb.velocity = new Vector3(_rb.velocity.x, _yVelocity, _rb.velocity.z);
    } // End Jump.

    public void HorizontalMovement(float xMovement)
    {
        _playerPos.x = xMovement * _maxPosX;
    } // End HorizontalMovement.

    private void Move(float percentage)
    {
        if (transform.position.z == _endVal)
        {
            return;
        } // End if.

        _playerPos.z = Mathf.Lerp(_startVal, _endVal, percentage);

        float xPos = Mathf.SmoothDamp(transform.position.x, _playerPos.x, ref _xVelocity, _smoothHorizontalTime);

        transform.position = new Vector3(xPos, transform.position.y, _playerPos.z);
    } // End Move.
} // End script.
