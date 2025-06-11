using UnityEngine;

public class FallState : State
{
    public FallState(PlayerController player) : base(player) { }

    public override void Update()
    {
        if (player.IsGrounded())
        {
            player.Abilities.ResetAirDash();
            if (player.Input.MoveInput != Vector2.zero)
                stateMachine.SwitchState(player.RunState);
            else
                stateMachine.SwitchState(player.IdleState);
        }

        if (player.Input.JumpPressed && player.CanUseDoubleJump())
        {
            stateMachine.SwitchState(player.JumpState, true);
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