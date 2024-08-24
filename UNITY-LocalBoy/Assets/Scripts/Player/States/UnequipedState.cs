using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipedState : PlayerBaseState
{
    public UnequipedState(PlayerStateMachine currentContext, Movement movement) : base(currentContext, movement)
    {
       
    }

    public override void EnterState()
    {
        movement.Speed = 3;
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        if (stateMachine.Grabbing)
        {
            SwitchState(stateMachine.exampleState);
        }
    }
}
