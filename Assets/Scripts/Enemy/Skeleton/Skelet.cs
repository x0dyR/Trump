using UnityEngine;
using UnityEngine.AI;
using System;

namespace collegeGame
{
    public class Skelet : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;
        public event Action Died;

        [SerializeField] private SkeletConfig _skeletConfig;
        [SerializeField] private SkeletView _view;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private int _maxOccupancy = 1;
        [SerializeField] private Transform[] _patrolPoints; // ������ ����� ��������������
        private NavMeshAgent _navAgent;
        private Transform _player;
        private int _currentPatrolIndex; // ������ ������� ����� ��������������
        private float _currentHealth;
        private bool isAttacking = false;
        private bool isAttackedThisFrame = false;

        public int MaxOccupancy
        {
            get { return _maxOccupancy; }
            set { _maxOccupancy = value; }
        }
        private void Start()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
            _currentPatrolIndex = -1; // �������� � -1, ����� ������� ��������� ����� �������������� ��� ������ ����������
            _currentHealth = _skeletConfig.Health;
            _view.Initialize(); // �������������� SkeletView
            SetDestinationToRandomPatrolPoint(); // �������� � �������������� � ��������� �����

        }

        private void Update()
        {
            isAttackedThisFrame = false;
            if (_player != null && ShouldChasePlayer())
            {
                // ���������� ������
                _navAgent.SetDestination(_player.position);
                _view.StartChase(); // ��������� �������� �������������
                _navAgent.speed = _skeletConfig.Speed;
            }
            if (Vector3.Distance(transform.position, _player.position) <= _skeletConfig.AttackRange)
            {
                // ���� ����� ���������, �������� �����
                _view.StartAttack(); // ��������� �������� �����
                AttackPlayer(); // �������� ����� ��� ���������� �����
            }
            else if (Vector3.Distance(transform.position, _player.position) <= 2f) // ���� ���������� �� ������ ������ ��� ����� 2 ������
            {
                _view.StartAttack(); // ��������� �������� �����
                AttackPlayer();                  // �������� ����� ��� ��� �����
            }
            else
            {
                // ����������� ����� �������
                if (_navAgent.remainingDistance < 0.1f)
                {
                    SetDestinationToRandomPatrolPoint();
                    _view.StartPatrol(); // ��������� �������� ��������������
                    _navAgent.speed = _skeletConfig.PatrolSpeed; // ������������� �������� ��������������
                }
            }
        }



        private void SetDestinationToRandomPatrolPoint()
        {
            if (_patrolPoints.Length == 0)
            {
                Debug.LogWarning("No patrol points available.");
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
            _navAgent.SetDestination(_patrolPoints[randomIndex].position);
        }


        private bool ShouldChasePlayer()
        {
            // ���������, ������ �� ������ ������������ ������
            if (_player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
                return distanceToPlayer < _skeletConfig.DistanceToView;
            }
            return false;
        }
        private void AttackPlayer()
        {
            // ������������ ���� � ��������� �����
            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _skeletConfig.AttackRange);
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Player") && isAttackedThisFrame)
                {
                    IHealth healthComponent = col.GetComponent<IHealth>(); // ���������, ���� �� � ������� ��������� ��������
                    if (healthComponent != null)
                    {
                        healthComponent.TakeDamage(_skeletConfig.EnemyDamage); // �������� ����� ��������� ����� � �������
                        isAttackedThisFrame = true;
                    }
                }
            }
        }


        public void TakeDamage(float damage)
        {

            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Died?.Invoke();
                _view.StartDead();
                //Destroy(gameObject);
                Invoke("DestroySkeleton", 3.0f); // ����� ����������� ����� 3 �������
            }
            HealthChanged?.Invoke();
            _healthbar.UpdateHealthBar(_currentHealth);
        }
        private void DestroySkeleton()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_attackPoint.position, _skeletConfig.AttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _skeletConfig.DistanceToView);
        }

        public float GetHealth()
        {
            return _currentHealth;
        }

    }
}



