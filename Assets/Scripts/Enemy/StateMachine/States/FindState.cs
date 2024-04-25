using UnityEngine;
using UnityEngine.AI;

namespace collegeGame.Enemy
{
    public class FindState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy enemy;
        private readonly EnemyConfig _config;
        private readonly ITarget player;
        private Vector3 target;

        public FindState(IStateSwitcher stateSwitcher, Enemy enemy, ITarget player)
        {
            StateSwitcher = stateSwitcher;
            this.enemy = enemy;
            _config = enemy.EnemyConfig;
            this.player = player;
        }

        public EnemyView View => enemy.View;

        public void Enter()
        {
            View.StartFind();
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = false;
            Collider[] colls = Physics.OverlapSphere(enemy.transform.position, _config.DistanceToView);
            foreach (Collider coll in colls)
            {
                if (coll.TryGetComponent(out ITarget target))
                {
                    if (target.Transform.position != enemy.Transform.position)
                        StateSwitcher.SwitchState<AttackState>();
                    return;
                }
            }

            NavMesh.SamplePosition(enemy.transform.position + Random.insideUnitSphere * _config.DistanceToView, out NavMeshHit hit, _config.DistanceToView, NavMesh.AllAreas);
            target = hit.position;
            enemy.NavAgent.SetDestination(hit.position);
        }


        public void Exit()
        {
            View.StopFind();//внутри класса EnemyView можно добавить какие то партиклы
        }

        public void Update()
        {
            if (Vector3.Distance(enemy.transform.position, target) < _config.AttackRange)
            {
                StateSwitcher.SwitchState<AttackState>();
            }
            if (Vector3.Distance(enemy.transform.position, target) > _config.AttackRange)
            {
                return;
            }
            StateSwitcher.SwitchState<IdleState>();
        }
    }
}
