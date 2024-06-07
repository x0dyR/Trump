using UnityEngine.InputSystem;

namespace Trump.StateMachine
{
    public class GroundedState : MovementState
    {
        private readonly GroundChecker _groundChecker;
        public GroundedState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _groundChecker = character.GroundChecker;

        public override void Enter()
        {
            base.Enter();

            View.StartGrounded();
        }
        public override void Exit()
        {
            base.Exit();

            View.StopGrounded();
        }

        public override void Update()
        {
            base.Update();

            if (_groundChecker.IsTouches)
                return;
            StateSwitcher.SwitchState<FallingState>();
        }
        protected override void AddInputActionsCallback()
        {
            base.AddInputActionsCallback();

            Input.Player.Jump.started += OnJump;
        }

        protected override void RemoveInputActionsCallback()
        {
            base.RemoveInputActionsCallback();

            Input.Player.Jump.started -= OnJump;
        }

        private void OnJump(InputAction.CallbackContext context) => StateSwitcher.SwitchState<JumpingState>();
    }
}
