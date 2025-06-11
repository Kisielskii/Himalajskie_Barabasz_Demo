using UnityEngine;

public class Abilities
{
    public Abilities()
    {
        UnlockJump();
        UnlockDoubleJump();
        UnlockDash();
    }

    public bool CanJump { get; private set; }
    public bool CanDoubleJump { get; private set; }
    public bool CanDash { get; private set; }
    public bool CanAirDash { get; private set; }

    public void UnlockJump() => CanJump = true;
    public void UnlockDoubleJump() => CanDoubleJump = true;
    public void UnlockDash()
    {
        CanDash = true;
        CanAirDash = true;
    }

    private bool hasAirDashed = false;
    private float dashCooldownTime = 0.5f;
    private float lastDashTime = -Mathf.Infinity;

    public bool CanUseDash(bool isGrounded)
    {
        if (Time.time < lastDashTime + dashCooldownTime) return false;
        if (isGrounded) return CanDash;
        return CanAirDash && !hasAirDashed;
    }

    public void UseDash(bool isGrounded)
    {
        lastDashTime = Time.time;
        if (!isGrounded) hasAirDashed = true;
    }

    public void ResetAirDash() => hasAirDashed = false;
}