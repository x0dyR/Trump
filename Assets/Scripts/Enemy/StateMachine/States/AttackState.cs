using UnityEngine;

namespace collegeGame.Enemy
{
    public class AttackState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy enemy;
        protected readonly ITarget target;
        private readonly EnemyConfig _config;
        private Collider[] colls;

        public AttackState(IStateSwitcher stateSwitcher, Enemy enemy, ITarget target)
        {
            StateSwitcher = stateSwitcher;
            this.enemy = enemy;
            this.target = target;
            _config = enemy.EnemyConfig;
        }

        public EnemyView View => enemy.View;

        public virtual void Enter()
        {
            View.StartAttack();
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = false;
            enemy.NavAgent.stoppingDistance = _config.AttackRange * 2;
            enemy.DealDamage(enemy.attackRange, _config.Damage);
        }

        public virtual void Exit()
        {
            View.StopAttack();
        }

        public virtual void LateUpdate()
        {
            colls = Physics.OverlapSphere(enemy.AttackPoint.position, _config.AttackRange);
            foreach(Collider coll in colls)
            {
                if (coll.TryGetComponent(out IHealth health))
                    enemy.DealDamage(enemy.attackRange, _config.Damage);
            }
        }

        public virtual void Update()
        {
            if (enemy.NavAgent.remainingDistance < _config.DistanceToView)
            {
                return;
            }

            StateSwitcher.SwitchState<FindState>();
        }

    }
}
