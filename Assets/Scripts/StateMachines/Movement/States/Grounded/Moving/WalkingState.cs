using System;
using UnityEngine.InputSystem;

namespace collegeGame
{
    public class WalkingState : GroundedState
    {
        public WalkingState(MovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IStates Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.MovementSpeedModifier = movementData.BaseWalkData.SpeedModifier;
        }
        #endregion


        #region Input Methods
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion
    }
}
