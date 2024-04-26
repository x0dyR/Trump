using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace collegeGame.Enemy
{
    public class Enemy : MonoBehaviour, IHealth, IDamage, INavAgent
    {
        public Vector3 attackRange = new(3f, 3f, 0f);
        [field: SerializeField] private EnemyConfig _enemyConfig;
        [field: SerializeField] private EnemyView _view;
        private NavMeshAgent _navAgent;
        private EnemyStateMachine _stateMachine;
        private ITarget player;
        private Health.Health _health;

        [Inject]
        private void Construct(ITarget target)
        {
            player = target;
        }

        public EnemyConfig EnemyConfig => _enemyConfig;

        public Transform Transform => transform;

        public NavMeshAgent NavAgent => _navAgent;

        public EnemyView View => _view;

        private void OnEnable() => _health.Die += OnDie;

        private void OnDisable() => _health.Die -= OnDie;

        private void OnDie()
        {
            //лучше сделать стейт
            //проигрываются партиклы, звуки, экран смерти
        }

        private void Awake()
        {
            _view.Initialize();
            _navAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new EnemyStateMachine(this, player);
            _navAgent.speed = _enemyConfig.Speed;
            _navAgent.stoppingDistance = _enemyConfig.AttackRange + 1;
            attackRange.z += _enemyConfig.AttackRange;
            _health = new(_enemyConfig.Health);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void DealDamage(Vector3 damageZone, float damage)
        {
            Collider[] colls = Physics.OverlapBox(transform.position, damageZone,transform.rotation);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out IHealth health))
                    health.TakeDamage(damage);
            }
        }
        public float GetHealth() => _health.HealthAmount;

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _enemyConfig.AttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _enemyConfig.DistanceToView);
            Gizmos.DrawWireCube(transform.position, attackRange);
        }
    }
}
