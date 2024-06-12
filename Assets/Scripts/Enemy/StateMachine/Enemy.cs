using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Trump.Enemy
{
    public class Enemy : MonoBehaviour, IHealth, IDamage, INavAgent
    {
        public Vector3 attackRange = new(3f, 3f, 0f);
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] private EnemyConfig _enemyConfig;
        [field: SerializeField] private EnemyView _view;
        [field: SerializeField] private Transform _attackPoint;
        private NavMeshAgent _navAgent;
        private EnemyStateMachine _stateMachine;
        private ITarget player;
        private IHealth _health;

        [Inject]
        private void Construct(ITarget target)
        {
            player = target;
        }

        public EnemyConfig EnemyConfig => _enemyConfig;

        public Transform Transform => transform;

        public NavMeshAgent NavAgent => _navAgent;

        public EnemyView View => _view;

        public Transform AttackPoint => _attackPoint;

        private void Awake()
        {
            /*            _view.Initialize();
                        _navAgent = GetComponent<NavMeshAgent>();
                        _stateMachine = new EnemyStateMachine(this, player);
                        _navAgent.speed = _enemyConfig.Speed;
                        _navAgent.stoppingDistance = _enemyConfig.AttackRange * 2;
                        _health = new HealthComponent(_enemyConfig.Health);*/
        }

        private void Update()
        {
            /*_stateMachine.Update();*/
        }

        private void LateUpdate()
        {
            /*            _stateMachine.LateUpdate();*/
        }

        public void DealDamage(Vector3 damageZone, float damage)
        {
            /*            Collider[] colls = Physics.OverlapBox(_attackPoint.position, damageZone, _attackPoint.rotation);
                        foreach (Collider coll in colls)
                        {
                            if (coll.TryGetComponent(out IHealth health))
                                health.TakeDamage(damage);
                        }*/
        }

        public float GetHealth() => _health.GetHealth();

        public void TakeDamage(float damage)
        {
            /*            _health.TakeDamage(damage);
                        HealthChanged?.Invoke();

                        if (_health.GetHealth() <= damage)
                        {
                            Died?.Invoke();
                        }*/
        }

        private void OnDrawGizmos()
        {
            /*            Gizmos.color = Color.red;
                        Gizmos.DrawSphere(_attackPoint.position, _enemyConfig.AttackRange);
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireSphere(transform.position, _enemyConfig.DistanceToView);*/
        }

        private void OnDie()
        {
            //����� ������� �����
            //������������� ��������, �����
        }

        private void OnEnable() => Died += OnDie;

        private void OnDisable() => Died -= OnDie;

        public void Heal(float heal)
        {
            throw new NotImplementedException();
        }
    }
}
