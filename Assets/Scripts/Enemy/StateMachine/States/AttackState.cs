using UnityEngine;

namespace collegeGame.Enemy
{
    public class AttackState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy enemy;
        protected readonly ITarget target;
        private readonly EnemyConfig _config;

        public AttackState(IStateSwitcher stateSwitcher, Enemy enemy, ITarget target)
        {
            StateSwitcher = stateSwitcher;
            this.enemy = enemy;
            this.target = target;
            _config = enemy.EnemyConfig;
        }

        public EnemyView View => enemy.View;

        public void Enter()
        {
            View.StartAttack();
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = false;

            Collider[] colls = Physics.OverlapSphere(enemy.transform.position, _config.DistanceToView);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget target))
                {
                    if (target.Transform.position != enemy.Transform.position)
                        enemy.NavAgent.SetDestination(this.target.Transform.position);
                }
            }

        }

        public void Exit()
        {
            View.StopAttack();
        }

        public void Update()
        {
            if (Vector3.Distance(enemy.transform.position, target.Transform.position) < _config.AttackRange)
            {
                return;
            }

            StateSwitcher.SwitchState<FindState>();

        }
    }
}
