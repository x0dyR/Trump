using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace collegeGame
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PersSystems : SystemBase
    {
        private PlayerInputActions _actions;
        private Entity _entity;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerMoveInput>();

            _actions = new PlayerInputActions();
        }
        protected override void OnStartRunning()
        {
            _actions.Enable();
        }

        protected override void OnUpdate()
        {
            var cur = _actions.Player.Move.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new PlayerMoveInput { Value = cur });
        }

        protected override void OnStopRunning()
        {
            _actions.Disable();

            _entity = Entity.Null;
        }
    }
}
