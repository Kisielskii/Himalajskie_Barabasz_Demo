using UnityEngine;

public class RunState : State
{
    public RunState(PlayerController player) : base(player) { }

    public override void Update()
    {
        if (player.Input.MoveInput == Vector2.zero)
        {
            stateMachine.SwitchState(player.IdleState);
        }

        if (player.Input.JumpPressed && player.Abilities.CanJump)
        {
            stateMachine.SwitchState(player.JumpState);
        }
    }

    public override void FixedUpdate()
    {
        player.Move(player.Input.MoveInput);
    }
}