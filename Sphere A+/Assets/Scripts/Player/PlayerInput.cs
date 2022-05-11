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

    [SerializeField] Slider _uiSlider;

    bool firstMove = false;

    public static event Action<float> OnPointerDrag;

    private void Awake()
    {
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    } // End Awake.

    public void ResetFirstMove()
    {
        firstMove = false;
    } // End ResetFirstMove.

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
