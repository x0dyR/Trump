using System.Collections.Generic;
using System.Linq;

namespace collegeGame.StateMachine
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public CharacterStateMachine(Character character)
        {
            StateMachineData data = new();

            _states = new()
            {
                new IdlingState(this,data,character),
                new RunningState(this,data,character),
                new FallingState(this,data,character),
                new JumpingState(this,data,character),
                new LightAttackState(this,data,character),

            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void HandleInput() => _currentState.HandleInput();

        public void Update() => _currentState.Update();

        public void LateUpdate() => _currentState.LateUpdate();
    }
}
