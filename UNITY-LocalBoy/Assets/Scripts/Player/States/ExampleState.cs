using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleState : PlayerBaseState
{
    // A constructor of this class that passes the context of the base state class
    public ExampleState(PlayerStateMachine currentContext, Movement movement) : base(currentContext, movement)
    {
        
    }

    /// <summary>
    /// Put here the logic it must follow like a void start
    /// </summary>
    public override void EnterState()
    {
        
    }

    /// <summary>
    /// Reset whatever you did at Enterstate
    /// </summary>
    public override void ExitState()
    {
        
    }

    /// <summary>
    /// Specify the conditions to leave the state, for this is useful you create bool variables in the statemachine and with a getter you pass it here
    /// </summary>
    public override void UpdateState()
    {
        
    }

    
}
