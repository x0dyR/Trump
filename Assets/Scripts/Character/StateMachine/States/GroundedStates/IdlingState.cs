namespace collegeGame.StateMachine
{
    public class IdlingState : GroundedState
    {
        public IdlingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        {
        }

        public override void Enter()
        {
            base.Enter();
            View.StartIdle();
            Data.Speed = 0;
        }
        public override void Exit()
        {
            base.Exit();

            View.StopIdle();
        }

        public override void Update()
        {
            base.Update();

            if (IsHorizontalInputZero())
                return;
            StateSwitcher.SwitchState<RunningState>();
        }
    }
}
