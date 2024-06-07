using UnityEngine;

namespace Trump.StateMachine
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
        public virtual void Enter() { View.StartAttacking(); _character.currentWeapon.Attack(); Cursor.lockState = CursorLockMode.Confined; Cursor.visible = false; }

        public virtual void Exit() { View.StopAttacking(); Data.attackThreshold = 0; }

        public virtual void HandleInput() { }

        public virtual void LateUpdate() { }

        public virtual void Update()
        {
            Data.attackThreshold += Time.deltaTime;
            if (Data.attackThreshold >= 1f)
                StateSwitcher.SwitchState<IdlingState>();
        }
    }
}