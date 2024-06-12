namespace Trump.Enemy
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void LateUpdate();
    }
}
