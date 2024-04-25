using UnityEngine;

namespace collegeGame.StateMachine
{
    public class AttackingState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly StateMachineData Data;
        protected readonly Animator _animator;

        private readonly Character _character;

        public AttackingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            _character = character;
        }

        protected PlayerInput Input => _character.Input;

        protected CharacterView View => _character.View;

        public virtual void Enter()
        {
            Debug.Log(GetType());
            Data.Speed = 0;

            View.StartAttacking();
        }

        public virtual void Exit()
        {
            View.StopAttacking();
        }

        public void HandleInput() { }

        public virtual void Update() { }

        public void LateUpdate() { }
    }
}
