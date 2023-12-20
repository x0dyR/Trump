namespace collegeGame
{
    public class MovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public StateReusableData ReusableData { get; }
        public IdlingState IdlingState { get; }
        public WalkingState WalkingState { get; }
        public RunningState RunningState { get; }
        public SprintState SprintState { get; }
        public MovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new StateReusableData();
            IdlingState = new IdlingState(this);
            WalkingState = new WalkingState(this);
            RunningState = new RunningState(this);
            SprintState = new SprintState(this);
        }
    }
}
