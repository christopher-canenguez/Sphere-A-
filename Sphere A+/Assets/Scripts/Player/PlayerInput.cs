using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnJump
    {
        get; private set;
    } // End UnityEvent.

    // Slider to allow player to move object from left to right.
    [SerializeField] Slider _uiSlider;

    bool firstMove = false;

    public static event Action<float> OnPointerDrag;

    // Get slider listener to check if it was moved throughout the running of the game.
    private void Awake()
    {
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    } // End Awake.

    public void ResetFirstMove()
    {
        firstMove = false;
    } // End ResetFirstMove.

    // If its the first move, make the player make its first jump.
    // Else, invoke slider action to control left to right action.
    public void OnSliderValueChanged(float value)
    {
        if (!firstMove)
        {
            firstMove = true;

            OnJump?.Invoke();
        } // End if.

        OnPointerDrag?.Invoke(value);
    } // End OnSliderValueChanged.
} // End script.
