using UnityEngine;

public class Abilities
{
    public Abilities()
    {
        UnlockJump();
    }

    public bool CanJump { get; private set; }
    public bool CanDoubleJump { get; private set; }
    public bool CanDash { get; private set; }

    public void UnlockJump() => CanJump = true;
    public void UnlockDoubleJump() => CanDoubleJump = true;
    public void UnlockDash() => CanDash = true;
}