using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace collegeGame
{
    public class EnemyTester : MonoBehaviour, IHealth, IDamage, INavAgent
    {
        public Vector3 damageZone;
        public float damage = 1;
        public float health = 100; public float restTimer = 5;
        public float restTime = 0;
        public IEnemyStates states;
        private NavMeshAgent _navAgent;
        private ITarget player;
        private IEnemyStates findState;
        private IEnemyStates chaseState;
        private IEnemyStates restState;

        public Transform Transform => transform;

        public float Speed => _navAgent.speed;

        public NavMeshAgent NavAgent => _navAgent;

        [Inject]
        private void Construct(ITarget target)
        {
            player = target;
        }

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            restState = new RestState();
            findState = new FindState(player, this);
            chaseState = new ChaseState(player, this);
            SetState(restState);
            states.StartMove();
        }

        public void DealDamage(Vector3 damageZone, float damage)
        {
            Vector3 center = new(damageZone.x / 2f, damageZone.y / 2f, damageZone.z / 2f);

            Collider[] colls = Physics.OverlapBox(transform.position, damageZone, transform.rotation);
            foreach (Collider coll in colls)
            {
                if (coll.transform.position != transform.position)
                {
                    if (coll.TryGetComponent(out IHealth target))
                        target.TakeDamage(damage);
                }
            }
        }

        private void Update()
        {
            states?.Update();
            if (states == restState)
            {
                restTime += Time.deltaTime;
            }
            if (states != restState)
            {
                CheckSphereForTarget();
                restTime = 0;
            }
        }


        public void SetState(IEnemyStates state)
        {
            states?.StopMove();
            states = state;
            states.StartMove();
        }

        public float GetHealth()
        {
            return health;
        }

        public void TakeDamage(float damage)
        {
            if (damage >= health)
            {
                Destroy(gameObject);
            }

            health -= damage;
        }

        private void FixedUpdate()
        {
            DealDamage(damageZone, damage);
        }

        /*        public void SetTarget()
                {
                    if (player != null)
                        StartChase();
                }

                public void StartChase()
                {
                    SetState(chaseState); restTime = 0;
                }
                public void Rest()
                {
                    SetState(restState); restTime += Time.deltaTime;
                }
                public void FindTarget()
                {
                    SetState(findState); restTime = 0;
                }*/

        private void CheckSphereForTarget()
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 20f);

            foreach (Collider coll in colls)
            {
                if (coll.transform != player.GetTransform())
                {
                    SetState(chaseState);
                }
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, damageZone);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 20f);
        }

    }
}
