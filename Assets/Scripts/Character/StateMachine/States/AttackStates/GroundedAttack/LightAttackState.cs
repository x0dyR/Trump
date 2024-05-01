namespace collegeGame.StateMachine
{
    public class LightAttackState : AttackingState
    {
        public LightAttackState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        { }

        public override void Enter()
        {
            base.Enter();

            View.StartLightAttack();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopLightAttack();
        }

        public override void Update()
        {
            base.Update();

            StateSwitcher.SwitchState<IdlingState>();
        }
    }
}
