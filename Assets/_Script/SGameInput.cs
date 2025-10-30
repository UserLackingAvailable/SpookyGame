using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SGameInput : MonoBehaviour
{
    public static SGameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnSprintAction;
    public event EventHandler OnFlashlightAction;

    private InputSystem_Actions playerInputActions;

    public bool IsInputEnabled { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        playerInputActions = new InputSystem_Actions();

        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.Sprint.performed += Sprint;
        playerInputActions.Player.Flashlight.performed += Flashlight;



        playerInputActions.Player.Enable();
        IsInputEnabled = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        if (playerInputActions != null)
        {
            playerInputActions.Player.Interact.performed -= Interact;
            playerInputActions.Player.Attack.performed -= Attack;
            playerInputActions.Player.Sprint.performed -= Sprint;
            playerInputActions.Player.Flashlight.performed -= Flashlight;
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        OnSprintAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Flashlight(InputAction.CallbackContext context)
    {
        OnFlashlightAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
    public Vector2 GetLookDelta()
    {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }

    public void EnableInput()
    {
        playerInputActions.Player.Enable();
        IsInputEnabled = true;
    }

    public void DisableInput()
    {
        playerInputActions.Player.Disable();
        IsInputEnabled = false;
    }
}
