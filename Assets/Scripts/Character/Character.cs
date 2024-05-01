using Cinemachine;
using collegeGame.Health;
using System;
using UnityEngine;

namespace collegeGame.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, ITarget, IHealth
    {
        public Transform cameraPoint;
        public Transform meeleWeaponSpawnPoint;
        public Weapon currentWeapon;
        public GameObject[] weapons;
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] private CharacterConfig _config;
        [field: SerializeField] private GroundChecker _groundChecker;
        [field: SerializeField] private CharacterView _view;
        private PlayerInput _input;
        private CharacterStateMachine _stateMachine;
        private CharacterController _characterController;
        private CinemachineVirtualCamera cm;
        private IHealth _health;

        public PlayerInput Input => _input;

        public CharacterController CharacterController => _characterController;

        public CharacterConfig Config => _config;

        public GroundChecker GroundChecker => _groundChecker;

        public CharacterView View => _view;

        Transform ITarget.Transform { get => transform; }

        private void Awake()
        {
            _health = new HealthComponent(50);
            _view.Initialize();
            _input = new();
            _characterController = GetComponent<CharacterController>();
            _stateMachine = new(this);
            BindVCam();
            Died += OnDie;
        }

        private void Update()
        {
            _stateMachine.HandleInput();
            _stateMachine.Update();
        }

        private void LateUpdate()
        {
            _stateMachine.LateUpdate();
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
            HealthChanged?.Invoke();

            if (_health.GetHealth() < damage)
            {
                Died?.Invoke();
            }
        }

        public float GetHealth() => _health.GetHealth();

        private void OnEnable() => _input.Enable();

        private void OnDisable() => _input.Disable();

        private void BindVCam()
        {
            cm = FindObjectOfType<CinemachineVirtualCamera>();
            cm.m_Follow = cameraPoint;
            cm.m_LookAt = cameraPoint;
        }
        private void OnDie()
        {
            _input.Disable();
        }
    }
}
