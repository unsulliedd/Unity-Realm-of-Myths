using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpInput = true;

        if (context.canceled)
            JumpInput = false;
    }
    public void JumpInputHelper() => JumpInput = false;

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;

        if (context.canceled)
            DashInput = false;
    }
    public void DashInputHelper() => DashInput = false;
}
