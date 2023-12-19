namespace collegeGame
{
    public interface IStates
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update();
        public void PhysicsUpdate();
    }
}
