using UnityEngine;

namespace Trump.StateMachine
{
    public class AirbornState : MovementState
    {
        private readonly AirbornStateConfig _config;

        public AirbornState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.AirbornStateConfig;

        public override void Enter()
        {
            base.Enter();

            View.StartAirborn();

            Data.Speed = _config.Speed;
        }

        public override void Exit()
        {
            base.Exit();

            View.StopAirborn();
        }
        public override void Update()
        {
            base.Update();

            Data.YVelocity -= _config.BaseGravity * Time.deltaTime;
        }
    }
}
