using Cinemachine;
using Trump.Health;
using System;
using UnityEngine;

namespace Trump.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, ITarget, IHealth
    {
        public Transform cameraPoint;
        public Transform meeleWeaponSpawnPoint;
        public IWeapon currentWeapon;
        public GameObject[] weapons;
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] private CharacterConfig _config;
        [field: SerializeField] private GroundChecker _groundChecker;
        [field: SerializeField] private CharacterView _view;
        private PlayerInput _input;
        private GameObject _currentWeaponInstance;
        private CharacterStateMachine _stateMachine;
        private CharacterController _characterController;
        private CinemachineVirtualCamera cm;
        private IHealth _health;
        private float _currentHealth;
        private float _maxHealth;

        public PlayerInput Input => _input;

        public CharacterController CharacterController => _characterController;

        public CharacterConfig Config => _config;

        public GroundChecker GroundChecker => _groundChecker;

        public CharacterView View => _view;

        Transform ITarget.Transform { get => transform; }

        private void Awake()
        {
            _health = new HealthComponent(_config.Health);
            _view.Initialize();
            _input = new();
            _characterController = GetComponent<CharacterController>();
            _stateMachine = new(this);
            BindVCam();
            Died += OnDie;
            currentWeapon = Instantiate(weapons[0], meeleWeaponSpawnPoint).GetComponent<IWeapon>();
            _currentHealth = _maxHealth = _health.GetHealth();

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
        public void Heal(float heal)
        {            
            _health.Heal(heal);
            HealthChanged?.Invoke();
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

        public void SwitchToWeapon(WeaponSO weaponData)
        {
            if (_currentWeaponInstance != null)
            {
                Destroy(_currentWeaponInstance);
            }

            GameObject weaponPrefab = Resources.Load<GameObject>(weaponData.Path);
            if (weaponPrefab == null)
            {
                Debug.LogError("Weapon prefab not found at path: " + weaponData.Path);
                return;
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
