using UnityEngine;

namespace collegeGame.Enemy
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

        public void Enter()
        {
            Debug.Log("Enemy " + GetType());
            enemy.NavAgent.isStopped = true;
            View.StartIdle();
        }

        public void Exit()
        {
            restTime = 0;
            View.StopIdle();
        }

        public void Update()
        {
            restTime += Time.deltaTime;
            if (restTime > restTimer)
            {
                StateSwitcher.SwitchState<FindState>();
            }
        }
    }
}