using UnityEngine;

namespace Trump.Enemy
{
    public class IdleState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy enemy;
        private float restTime = 0;
        private float restTimer = 5;


        public IdleState(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            StateSwitcher = stateSwitcher;
            this.enemy = enemy;
        }

        public EnemyView View => enemy.View;

        public virtual void Enter()
        {
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = true;
            View.StartIdle();
        }

        public virtual void Exit()
        {
            restTime = 0;
            View.StopIdle();
        }

        public virtual void LateUpdate() { }
        public virtual void Update()
        {
            restTime += Time.deltaTime;
            if (restTime > restTimer)
            {
                StateSwitcher.SwitchState<FindState>();
            }
        }
    }
}