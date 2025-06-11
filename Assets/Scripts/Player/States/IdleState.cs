using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerController player) : base(player){}

    public override void Enter()
    {
        player.Rigidbody.linearVelocity = new Vector3(0, player.Rigidbody.linearVelocity.y);
        player.ResetDoubleJump();
    }
    public override void Update()
    {
        if (player.Input.MoveInput != Vector2.zero)
        {
            stateMachine.SwitchState(player.RunState);
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
}