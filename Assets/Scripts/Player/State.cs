using UnityEngine;

public abstract class State
{
    public PlayerController player;
    public InputHandler input;
    public StateMachine stateMachine;

    public State(PlayerController player)
    {
        this.player = player;
        stateMachine = player.StateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
