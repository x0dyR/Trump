using UnityEngine.InputSystem;

namespace collegeGame
{
    public class RunningState : GroundedState
    {
        public RunningState(MovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IStates Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.MovementSpeedModifier = movementData.BaseRunData.SpeedModifier;
        }
        #endregion


        #region Input Methods
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        #endregion

    }
}
