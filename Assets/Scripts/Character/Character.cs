using Cinemachine;
using UnityEngine;

namespace collegeGame.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, ITarget, IHealth
    {
        [field: SerializeField] private CharacterConfig _config;
        [field: SerializeField] private GroundChecker _groundChecker;
        [field: SerializeField] private CharacterView _view;
        private PlayerInput _input;
        private CharacterStateMachine _stateMachine;
        private CharacterController _characterController;
        private CinemachineVirtualCamera cm;
        private Health.Health _health;

        public Transform cameraPoint;
        public Transform meeleWeaponSpawnPoint;

        public Weapon currentWeapon;
        public GameObject[] weapons;

        public PlayerInput Input => _input;

        public CharacterController CharacterController => _characterController;

        public CharacterConfig Config => _config;

        public GroundChecker GroundChecker => _groundChecker;

        public CharacterView View => _view;

        Transform ITarget.Transform { get => transform; }

        private void Awake()
        {
            _health = new(50);
            _view.Initialize();
            _input = new();
            _characterController = GetComponent<CharacterController>();
            _stateMachine = new(this);
            cm = FindObjectOfType<CinemachineVirtualCamera>();
            cm.m_Follow = cameraPoint;
            cm.m_LookAt = cameraPoint;

            foreach (var weapon in weapons)
            {
                Instantiate(weapon, meeleWeaponSpawnPoint);
                weapon.transform.localScale = new(0.01f, 0.01f, 0.01f);
                weapon.SetActive(false);
            }
            weapons[0].SetActive(true);
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

        private void OnEnable() => _input.Enable();


        private void OnDisable() => _input.Disable();


        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public float GetHealth() => _health.HealthAmount;
    }
}
