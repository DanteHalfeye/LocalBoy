using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipedState : PlayerBaseState
{
    public UnequipedState(PlayerStateMachine currentContext, Movement movement, PlayerActor actor) : base(currentContext, movement, actor)
    {
       
    }

    public override void EnterState()
    {
        actor.SetMovingSpeed(3);
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
