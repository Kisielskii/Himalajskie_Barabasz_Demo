using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        if (player.IsJumpAllowed())
        {
            player.Jump();
            player.ResetDoubleJump();
        }
        else if (player.CanUseDoubleJump())
        {
            player.UseDoubleJump();
            player.Jump();
        }
    }

    public override void Update()
    {
        if (player.Input.JumpPressed && player.CanUseDoubleJump())
        {
            stateMachine.SwitchState(player.JumpState, true); // trigger double jump
        }

        if (player.IsFalling())
        {
            stateMachine.SwitchState(player.FallState);
        }

        if (player.Input.DashPressed && player.Abilities.CanUseDash(player.IsGrounded()))
        {
            stateMachine.SwitchState(player.DashState);
        }
    }

    public override void FixedUpdate()
    {
        player.Move(player.Input.MoveInput);
    }
}