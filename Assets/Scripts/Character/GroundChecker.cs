using UnityEngine;

namespace collegeGame
{
    public class GroundChecker : MonoBehaviour
    {
        [field: SerializeField] private LayerMask _ground;

        [field: SerializeField, Range(0.01f, 1)] private float _distanceToCheck;

        public bool IsTouches { get; private set; }


        public void Update()
        {
            IsTouches = Physics.CheckSphere(transform.position, _distanceToCheck, _ground, QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmos() => Gizmos.DrawSphere(transform.position, _distanceToCheck);
    }
}
