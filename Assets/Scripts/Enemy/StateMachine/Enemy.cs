using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace collegeGame.Enemy
{
    public class Enemy : MonoBehaviour, IHealth, IDamage, INavAgent
    {
        [field: SerializeField] private EnemyConfig _enemyConfig;
        [field:SerializeField] private EnemyView _view;
        private NavMeshAgent _navAgent;        
        private EnemyStateMachine _stateMachine;
        private ITarget player;

        [Inject]
        private void Construct(ITarget target)
        {
            player = target;
        }

        public EnemyConfig EnemyConfig => _enemyConfig;

        public Transform Transform => transform;

        public NavMeshAgent NavAgent => _navAgent;

        public EnemyView View => _view;

        private void Awake()
        {
            _view.Initialize();
            _navAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new EnemyStateMachine(this, player);
            _navAgent.speed = _enemyConfig.Speed;
            _navAgent.stoppingDistance = _enemyConfig.AttackRange;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void DealDamage(Vector3 damageZone, float damage)
        {
            throw new System.NotImplementedException();
        }

        public float GetHealth()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _enemyConfig.AttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _enemyConfig.DistanceToView);
        }
    }
}
