using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerController player) : base(player){}

    public override void Enter()
    {
        player.Rigidbody.linearVelocity = new Vector3(0, player.Rigidbody.linearVelocity.y);
    }
    public override void Update()
    {
        if (player.Input.MoveInput != Vector2.zero)
        {
            stateMachine.SwitchState(player.RunState);
        }

        if (player.Input.JumpPressed && player.Abilities.CanJump)
        {
            stateMachine.SwitchState(player.JumpState);
        }
    }
}
