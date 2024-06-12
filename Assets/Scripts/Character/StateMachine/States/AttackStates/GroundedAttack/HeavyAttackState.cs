namespace Trump.StateMachine
{
    public class HeavyAttackState : AttackingState
    {
        public HeavyAttackState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        { }

        public override void Enter()
        {
            base.Enter();

            View.StartHeavyAttack();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopHeavyAttack();
        }
    }
}
