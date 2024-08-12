
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Vector2 moveDirection = Vector2.zero;
    bool jumpPressed = false;
    bool cycleElementPressed = false;
    bool ability1Pressed = false;
    bool ability2Pressed = false;
    bool ability3Pressed = false;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        Instance = this;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public bool GetJumpPressed()
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }
    public void CycleElementPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cycleElementPressed = true;
        }
        else if (context.canceled)
        {
            cycleElementPressed = false;
        }
    }

    public bool GetCycleElementPressed()
    {
        bool result = cycleElementPressed;
        cycleElementPressed = false;
        return result;
    }

    public void Ability1Pressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ability1Pressed = true;
        }
        else if (context.canceled)
        {
            ability1Pressed = false;
        }
    }

    public bool GetAbility1Pressed()
    {
        bool result = ability1Pressed;
        ability1Pressed = false;
        return result;
    }

    public void Ability2Pressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ability2Pressed = true;
        }
        else if (context.canceled)
        {
            ability2Pressed = false;
        }
    }

    public bool GetAbility2Pressed()
    {
        bool result = ability2Pressed;
        ability2Pressed = false;
        return result;
    }

    public void Ability3Pressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ability3Pressed = true;
        }
        else if (context.canceled)
        {
            ability3Pressed = false;
        }
    }

    public bool GetAbility3Pressed()
    {
        bool result = ability3Pressed;
        ability3Pressed = false;
        return result;
    }
}
