using UnityEngine;

public class FallState : State
{
    public FallState(PlayerController player) : base(player) { }

    public override void Update()
    {
        if (player.IsGrounded())
        {
            if (player.Input.MoveInput != Vector2.zero)
                stateMachine.SwitchState(player.RunState);
            else
                stateMachine.SwitchState(player.IdleState);
        }

        if (player.Input.JumpPressed && player.CanUseDoubleJump())
        {
            stateMachine.SwitchState(player.JumpState, true);
        }
    }

    public override void FixedUpdate()
    {
        player.Move(player.Input.MoveInput);
    }
}