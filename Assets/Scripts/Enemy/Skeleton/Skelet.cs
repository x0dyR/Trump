using UnityEngine;
using UnityEngine.AI;
using System;
using Zenject;
using Trump.StateMachine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Trump
{
    public class Skelet : AbsEnemy, IHealth
    {
        public event Action HealthChanged;
        public event Action Died;

        [SerializeField] private EnemyConfig _skeletConfig;
        [SerializeField] private SkeletView _view;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private int _maxOccupancy = 1;
        [SerializeField] private List<Transform> _patrolPoints; // Массив точек патрулирования
        [field: SerializeField] private int _patrolPointsCount;
        private NavMeshAgent _navAgent;
        private Character _player;
        private int _currentPatrolIndex; // Индекс текущей точки патрулирования
        private float _currentHealth;
        private bool isAttacking = false;
        private bool isAttackedThisFrame = false;
        private float _maxHealth;

        public int MaxOccupancy
        {
            get { return _maxOccupancy; }
            set { _maxOccupancy = value; }
        }

        [Inject]
        private void Construct(Character character)
        {
            _player = character;
        }

        private void Start()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            CreatePatrolPoints(_patrolPoints);
            _currentPatrolIndex = 0; // Начинаем с -1, чтобы выбрать случайную точку патрулирования при первом обновлении
            _maxHealth = _currentHealth = _skeletConfig.Health;
            _view.Initialize(); // Инициализируем SkeletView
            SetDestinationToRandomPatrolPoint(); // Начинаем с патрулирования к случайной точке
            _maxHealth = _skeletConfig.Health;
        }

        public List<Transform> CreatePatrolPoints(List<Transform> patrolArray)
        {
            if (patrolArray.Count > 0)
                return patrolArray;
            patrolArray.Clear();

            for (int i = 0; i < _patrolPointsCount; i++)
            {
                GameObject asd = new("Patrol Point");
                asd.transform.parent = transform;
                NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * _skeletConfig.DistanceToView, out NavMeshHit hit, _skeletConfig.DistanceToView, NavMesh.AllAreas);
                asd.transform.position = hit.position;
                patrolArray.Add(asd.transform);
            }
            return patrolArray;
        }

        private void Update()
        {
            isAttackedThisFrame = false;
            if (_player != null && ShouldChasePlayer())
            {
                // Преследуем игрока
                _navAgent.SetDestination(_player.transform.position);
                _view.StartChase(); // Запускаем анимацию преследования
                _navAgent.speed = _skeletConfig.Speed;
            }
            else if (Vector3.Distance(transform.position, _player.transform.position) <= _skeletConfig.AttackRange)
            {
                // Если игрок достигнут, начинаем атаку
                _view.StartAttack(); // Запускаем анимацию атаки
                AttackPlayer(); // Вызываем метод для выполнения атаки
            }
            else if (_navAgent.remainingDistance < 0.1f)
            {
                SetDestinationToRandomPatrolPoint();
                _view.StartPatrol(); // Запускаем анимацию патрулирования
                _navAgent.speed = _skeletConfig.PatrolSpeed; // Устанавливаем скорость патрулирования
            }
        }

        private void SetDestinationToRandomPatrolPoint()
        {
            if (_patrolPoints.Count == 0)
            {
                Debug.LogWarning("No patrol points available.");
                return;
            }

            int randomIndex = Random.Range(0, _patrolPoints.Count);
            _navAgent.SetDestination(_patrolPoints[randomIndex].position);
        }

        private bool ShouldChasePlayer()
        {
            // Проверяем, должен ли скелет преследовать игрока
            if (_player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
                return distanceToPlayer < _skeletConfig.DistanceToView;
            }
            return false;
        }

        private void AttackPlayer()
        {
            // Регистрируем урон в атакующей точке
            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _skeletConfig.AttackRange);
            foreach (Collider col in hitColliders)
            {
                if (!isAttackedThisFrame)
                {
                    _player.TakeDamage(_skeletConfig.Damage);
                }
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Died?.Invoke();
                _view.StartDead();
                Destroy(gameObject, 3);
            }
            HealthChanged?.Invoke();
            _healthbar.UpdateHealthBar(_currentHealth);
        }

        private void DestroySkeleton()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_attackPoint.position, _skeletConfig.AttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _skeletConfig.DistanceToView);
        }

        public float GetHealth()
        {
            return _currentHealth;
        }

        public void Heal(float heal)
        {
            if (_currentHealth > _maxHealth)
                return;
            _currentHealth += heal;
        }
    }
}
