namespace collegeGame.StateMachine
{
    public class LightAttackState : AttackingState
    {
        private readonly Character _character;

        public LightAttackState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        { }
        /*=> _character = character;*/
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            StateSwitcher.SwitchState<IdlingState>();
        }
    }
}
