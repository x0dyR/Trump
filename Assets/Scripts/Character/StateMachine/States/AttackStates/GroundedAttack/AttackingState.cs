namespace collegeGame.StateMachine
{
    public class AttackingState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly StateMachineData Data;

        private readonly Character _character;

        public AttackingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            _character = character;
        }

        protected CharacterView View => _character.View;
        public virtual void Enter() => View.StartAttacking();

        public virtual void Exit() => View.StopAttacking();

        public virtual void HandleInput() { }

        public virtual void LateUpdate() { }

        public virtual void Update() { }
    }
}