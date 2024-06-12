using UnityEngine;

namespace Trump.StateMachine
{
    public abstract class MovementState : IState
    {
        public float RotationSmoothTime = .12f;

        protected readonly IStateSwitcher StateSwitcher;
        protected readonly StateMachineData Data;

        private float _rotationVelocity;
        private readonly Character _character;

        public MovementState(IStateSwitcher stateSwitcher, StateMachineData data, Character character)
        {
            StateSwitcher = stateSwitcher;
            _character = character;
            Data = data;
        }

        protected PlayerInput Input => _character.Input;
        protected CharacterController CharacterController => _character.CharacterController;

        protected CharacterView View => _character.View;

        protected bool IsHorizontalInputZero() => ReadHorizontalInput() == Vector2.zero;

        public virtual void Enter()
        {
            Debug.Log(GetType());
            AddInputActionsCallback();
        }

        public virtual void Exit() => RemoveInputActionsCallback();

        public void HandleInput()
        {
            Data.XInput = ReadHorizontalInput().x;
            Data.ZInput = ReadHorizontalInput().y;
            Data.XVelocity = Data.XInput * Data.Speed;
            Data.ZVelocity = Data.ZInput * Data.Speed;
        }

        public virtual void Update()
        {
            MoveCharacter();
        }

        public virtual void LateUpdate() => Camera.main.transform.rotation = Quaternion.Euler(ReadCameraInput().x, ReadCameraInput().y, 0);

        protected virtual void AddInputActionsCallback()
        {
            Input.Player.SwitchToSword.started += ctx => _character.SwitchToWeapon(Resources.Load<WeaponSO>("Sword"));
            Input.Player.SwitchToPolearm.started += ctx => _character.SwitchToWeapon(Resources.Load<WeaponSO>("Polearm"));
            Input.Player.LightAttack.started += OnLightAttack;
            Input.Player.HeavyAttack.started += OnHeavyAttack;
        }

        protected virtual void RemoveInputActionsCallback() {
            Input.Player.SwitchToSword.started -= ctx => _character.SwitchToWeapon(Resources.Load<WeaponSO>("Sword"));
            Input.Player.SwitchToPolearm.started -= ctx => _character.SwitchToWeapon(Resources.Load<WeaponSO>("Polearm"));
            Input.Player.LightAttack.started -= OnLightAttack;
            Input.Player.HeavyAttack.started -= OnHeavyAttack;
        }

        private Vector2 ReadHorizontalInput() => Input.Player.Movement.ReadValue<Vector2>().normalized;

        private Vector2 ReadCameraInput() => Input.Player.Look.ReadValue<Vector2>();

        private void MoveCharacter()
        {
            Vector3 moveDirection = (Camera.main.transform.forward * Data.ZVelocity) + (Camera.main.transform.right * Data.XVelocity);
            moveDirection.y = Data.YVelocity;
            CharacterController.Move(moveDirection * Time.deltaTime);

            if (Data.ZInput > .1f || Data.XInput > .1f || Data.ZInput <= -0.5 || Data.XInput <= -0.5)
            {
                float targetRotation = Mathf.Atan2(Data.XVelocity, Data.ZVelocity) * Mathf.Rad2Deg + Camera.main.transform.rotation.eulerAngles.y;

                _character.transform.rotation = Quaternion.Euler(0.0f, Mathf.SmoothDampAngle(_character.transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSmoothTime), 0.0f);
            }
        }

        private void OnLightAttack(UnityEngine.InputSystem.InputAction.CallbackContext context) => StateSwitcher.SwitchState<LightAttackState>();

        private void OnHeavyAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj) => StateSwitcher.SwitchState<HeavyAttackState>();

    }
}
