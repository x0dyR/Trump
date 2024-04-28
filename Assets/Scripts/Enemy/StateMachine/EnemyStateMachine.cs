using System.Collections.Generic;
using System.Linq;

namespace collegeGame.Enemy
{
    public class EnemyStateMachine : IStateSwitcher
    {
        public ITarget target;
        private List<IState> _states;
        private IState _currentState;

        public EnemyStateMachine(Enemy enemy,ITarget target)
        {
            _states = new()
            {
                new IdleState(this,enemy),
                new FindState(this,enemy,target),
                new AttackState(this,enemy,target),
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

        public void Update() => _currentState.Update();
    }
}
