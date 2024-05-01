namespace collegeGame.Enemy
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void LateUpdate();
    }
}
