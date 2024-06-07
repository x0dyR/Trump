using UnityEngine;
using UnityEngine.AI;
using System;

namespace Trump
{
    public class Troll : AbsEnemy, IHealth
    {
        public event Action HealthChanged;
        public event Action Died;

        [SerializeField] private EnemyConfig _trollConfig;
        [SerializeField] private TrollView _view;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Healthbar_t _healthbar;
        [SerializeField] private Transform[] _patrolPoints;
        private NavMeshAgent _navAgent;
        private Transform _player;
        private int _currentPatrolIndex = 0;
        private float _currentHealth;
        private bool isAttackedThisFrame = false;
        private float _maxHealth;

        private void Start()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
            _maxHealth = _currentHealth = _trollConfig.Health;
            _view.Initialize();
            _view.StartPatrol();
            Patrol();
        }

        private void Update()
        {
            isAttackedThisFrame = false;
            if (_player != null && ShouldChasePlayer())
            {
                _navAgent.SetDestination(_player.position);
                _view.StartChase();
                _navAgent.speed = _trollConfig.Speed;
            }
            if (Vector3.Distance(transform.position, _player.position) <= _trollConfig.AttackRange)
            {
                _view.StartAttack();
                AttackPlayer();
            }
            else if (Vector3.Distance(transform.position, _player.position) <= 4f) // ≈сли рассто€ние до игрока меньше или равно 4 метрам
            {
                _view.StartAttack(); // «апускаем анимацию атаки
                AttackPlayer();
            }
            else
            {
                if (_navAgent.remainingDistance < 0.1f && !_navAgent.pathPending)
                {
                    Patrol();
                }
            }
        }

        private void Patrol()
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
            _navAgent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
            _view.StartPatrol();
            _navAgent.speed = _trollConfig.PatrolSpeed;
        }

        private bool ShouldChasePlayer()
        {
            if (_player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
                return distanceToPlayer < _trollConfig.DistanceToView;
            }
            return false;
        }

        private void AttackPlayer()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _trollConfig.AttackRange);
            foreach (Collider col in hitColliders)
            {
                if (col.TryGetComponent(out ITarget character) && !isAttackedThisFrame)
                {
                    if(col.TryGetComponent(out IHealth health))
                    {
                        health.TakeDamage(_trollConfig.EnemyDamage);
                        isAttackedThisFrame = true;
                    }
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
                Invoke("DestroyTroll", 3.0f);
            }
            HealthChanged?.Invoke();
            _healthbar.UpdateHealthBar(_currentHealth);
        }

        private void DestroyTroll()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_attackPoint.position, _trollConfig.AttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _trollConfig.DistanceToView);
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

