using UnityEngine;

public class DashState : State
{
    public DashState(PlayerController player) : base(player) { }

    private float dashDuration = 0.2f;
    private float dashSpeed = 15f;
    private float timer = 0f;
    private Vector2 dashDirection;

    public override void Enter()
    {
        timer = 0f;
        player.transform.rotation = player.targetRotation;
        dashDirection = player.Input.MoveInput.normalized;
        if (dashDirection == Vector2.zero)
            dashDirection = new Vector2(player.isFacingRight ? 1 : -1, 0);

        player.Abilities.UseDash(player.IsGrounded());
        player.Rigidbody.useGravity = false;
        player.SetVelocity(dashDirection * dashSpeed);
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dashDuration)
        {
            player.Rigidbody.useGravity = true;
            if (!player.IsGrounded())
                stateMachine.SwitchState(player.FallState);
            else if (player.Input.MoveInput != Vector2.zero)
                stateMachine.SwitchState(player.RunState);
            else
                stateMachine.SwitchState(player.IdleState);
        }
    }

    public override void Exit()
    {
        player.Rigidbody.useGravity = true;
    }
}