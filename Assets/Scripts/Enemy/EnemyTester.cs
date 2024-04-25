using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public class EnemyTester : MonoBehaviour
    {
        private NavMeshAgent _navAgent;
        [Range(0,30)]public float revealRange = 30f;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * revealRange, out NavMeshHit hit, revealRange, NavMesh.AllAreas);
            _navAgent.SetDestination(hit.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, revealRange);
        }

    }
}
