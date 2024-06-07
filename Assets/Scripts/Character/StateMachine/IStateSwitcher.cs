namespace Trump.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
