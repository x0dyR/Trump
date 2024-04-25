namespace collegeGame.Enemy
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
