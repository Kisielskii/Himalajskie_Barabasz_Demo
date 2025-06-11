using UnityEngine;

public class RunState : State
{
    public RunState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.ResetDoubleJump();
    }

    public override void Update()
    {
        if (player.Input.MoveInput == Vector2.zero)
        {
            stateMachine.SwitchState(player.IdleState);
        }

        if (Time.time - player.lastJumpPressedTime <= player.jumpBufferTime &&
            player.Abilities.CanJump &&
            player.IsJumpAllowed())
        {
            stateMachine.SwitchState(player.JumpState);
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