using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    // We save the state machine in order to tell it to change states
    protected PlayerStateMachine stateMachine;

    /// <summary>
    /// This is the constructor class - if you need reference to something like the input system just add the argument to the method constuctor
    /// </summary>

    public PlayerBaseState(PlayerStateMachine currentContext)
    {
        stateMachine = currentContext;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    /// <summary>
    /// Handles the transitions between states
    /// </summary>
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        newState.EnterState();

        stateMachine.CurrentState = newState;
    }
}
