using UnityEngine.AI;
using UnityEngine;

namespace collegeGame
{
    public class FindState : IEnemyStates
    {
        public float offset = .05f;

        private ITarget target;
        private INavAgent self;
        private bool _isMoving;

        public FindState(ITarget target, INavAgent self)
        {
            this.target = target;
            this.self = self;
        }
        public void StartMove()
        {
            Debug.Log("Find");
            _isMoving = true;
            self.NavAgent.isStopped = true;
            Vector3 randomDirection = Random.insideUnitSphere * 20f + self.Transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 20f, NavMesh.AllAreas))
            {
                self.NavAgent.SetDestination(hit.position);
            }
        }

        public void StopMove()
        {
            _isMoving = false; self.NavAgent.isStopped = false; Debug.Log("Next state");
        }

        public void Update()
        {
            if (_isMoving == false)
                return;

        }
    }
}