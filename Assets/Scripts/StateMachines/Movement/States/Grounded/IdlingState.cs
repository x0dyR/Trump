using System;
using UnityEngine;

namespace collegeGame
{
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IStates Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
            ResetVelocity();
        }
        public override void Update()
        {
            base.Update();
            if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                return;
            }
            OnMove();
        }


        #endregion
    }
}
