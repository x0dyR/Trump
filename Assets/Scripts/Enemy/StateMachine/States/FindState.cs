using UnityEngine;
using UnityEngine.AI;

namespace Trump.Enemy
{
    public class FindState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy enemy;
        private readonly EnemyConfig _config;
        private readonly ITarget player;
        private Vector3 target;
        private Collider[] colls;

        public FindState(IStateSwitcher stateSwitcher, Enemy enemy, ITarget player)
        {
            StateSwitcher = stateSwitcher;
            this.enemy = enemy;
            _config = enemy.EnemyConfig;
            this.player = player;
        }

        public EnemyView View => enemy.View;

        public virtual void Enter()
        {
            View.StartFind();
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = false;
            enemy.NavAgent.stoppingDistance = 0;
            Collider[] colls = Physics.OverlapSphere(enemy.transform.position, _config.DistanceToView);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget target))
                {
                    if (target.Transform.position != enemy.Transform.position)
                    {
                        this.target = target.Transform.position;
                        enemy.NavAgent.SetDestination(this.target);
                        StateSwitcher.SwitchState<AttackState>();
                        return;
                    }
                }
            }

            NavMesh.SamplePosition(enemy.transform.position + Random.insideUnitSphere * _config.DistanceToView, out NavMeshHit hit, _config.DistanceToView, NavMesh.AllAreas);
            target = hit.position;
            enemy.NavAgent.SetDestination(target);
        }


        public virtual void Exit()
        {
            View.StopFind();//внутри класса EnemyView можно добавить какие то партиклы
        }

        public void LateUpdate()
        {
            colls = Physics.OverlapSphere(enemy.AttackPoint.position, _config.DistanceToView);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget target))
                    enemy.NavAgent.SetDestination(target.Transform.position);
                else
                    enemy.NavAgent.SetDestination(this.target);
            }
        }

        public virtual void Update()
        {
            if (enemy.NavAgent.remainingDistance < _config.AttackRange)
            {
                StateSwitcher.SwitchState<AttackState>();
            }

            if (enemy.NavAgent.remainingDistance > _config.AttackRange)
            {
                return;
            }

            StateSwitcher.SwitchState<IdleState>();
        }
    }
}
