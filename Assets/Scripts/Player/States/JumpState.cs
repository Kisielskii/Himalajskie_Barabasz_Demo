using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerController player) : base(player) { }

    private bool hasJumped;

    public override void Enter()
    {
        player.Jump();
        hasJumped = true;
    }

    public override void Update()
    {
        if (player.IsGrounded() && hasJumped)
        {
            // Landed
            if (player.Input.MoveInput != Vector2.zero)
                stateMachine.SwitchState(player.RunState);
            else
                stateMachine.SwitchState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        player.Move(player.Input.MoveInput);
    }
}