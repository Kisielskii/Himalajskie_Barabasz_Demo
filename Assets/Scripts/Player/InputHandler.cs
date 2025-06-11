using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    private Controls controls;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool DashPressed { get; private set; }

    public InputHandler()
    {
        controls = new Controls();
        controls.Player.Enable();
    }

    public void Update()
    {
        MoveInput = controls.Player.Move.ReadValue<Vector2>();
        JumpPressed = controls.Player.Jump.WasPressedThisFrame();
        JumpHeld = controls.Player.Jump.IsPressed();
        DashPressed = controls.Player.Dash.WasPressedThisFrame();
    }
}
