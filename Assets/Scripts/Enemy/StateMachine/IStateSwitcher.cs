namespace Trump.Enemy
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
