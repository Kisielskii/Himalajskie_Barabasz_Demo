using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private InputHandler input;

    void Start()
    {
        SetState(new Idle()); // Start in idle state
    }

    void Update()
    {
        input.Update();
        currentState.Update();
        currentState.FixedUpdate();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void SetState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
