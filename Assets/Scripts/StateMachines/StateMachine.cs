namespace collegeGame
{
    public abstract class StateMachine
    {
        protected IStates currentState;

        public void ChangeState(IStates state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
        public void HandleInput()
        {
            currentState?.HandleInput();
        }
        public void Update()
        {
            currentState?.Update();
        }
        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}
