using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public class ChaseState : IEnemyStates
    {
        private ITarget target;
        private INavAgent self;
        private bool _isMoving;
        private float attackRange;

        public ChaseState(ITarget target, INavAgent self)
        {
            this.target = target;
            this.self = self;
        }
        public void StartMove()
        {
            _isMoving = true;
            self.NavAgent.isStopped = false;
            if (Vector3.Distance(self.Transform.position, target.GetTransform().position) < 20)
                self.NavAgent.SetDestination(target.GetTransform().position);
            else
            {
                Vector3 randomDirection = Random.insideUnitSphere * 20f + self.Transform.position;
                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 20f, NavMesh.AllAreas))
                {
                    self.NavAgent.SetDestination(hit.position);
                }
            }
            Debug.Log("Chase");
        }

        public void StopMove()
        {
            _isMoving = false;
            self.NavAgent.isStopped = true; Debug.Log("Next state");
        }

        public void Update()
        {
            if (_isMoving == false)
                return;
/*            Collider[] colls = Physics.OverlapSphere(self.Transform.position, 20f);

            foreach (Collider coll in colls)
            {
                if (coll.transform == target.GetTransform())
                {
                    self.NavAgent.SetDestination(coll.transform.position);
                }
                else
                    return;

            }*/
        }
    }
}
