using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;
using collegeGame;
public class Troll : MonoBehaviour, INavAgent
{
    private Animator animator;

    private NavMeshAgent navMeshAgent;
    private IEnemyStates restState;
    private IEnemyStates chaseState;
    private IEnemyStates findState;

    private float _health = 100;
    private float _damage = 20;
    private float defaultChaseRadius = 10f;
    private float extendedChaseRadius = 20f;
    private float currentChaseRadius;

    private bool isResting = false;
    private bool isChasing = false;
    private bool isDead = false; // Добавляем флаг для отслеживания смерти тролля

    public float chaseThreshold = 1.5f;
    public float idleSpeedThreshold = 0.1f;
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5f;
    public float idleTimer = 0;
    public float idleTime = 5f;

    public Transform attackPoint;

    private ITarget player;

    public Transform Transform => transform;

    public float Speed => navMeshAgent.speed;

    public NavMeshAgent NavAgent => navMeshAgent;

    [Inject]
    private void Construct(ITarget target)
    {
        player = target;
    }

    private void Awake()
    {
        chaseState = new ChaseState(player,this);
        findState = new FindState(player, this);
        restState = new RestState();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentChaseRadius = defaultChaseRadius;
        navMeshAgent.speed = walkingSpeed;
        SetRandomDestination();
    }

    void Update()
    {
        /*        if (!isDead) // Проверяем, жив ли тролль, прежде чем выполнять его обновление
                {
                    if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f && !isResting)
                        StartCoroutine(Rest());

                    if (isChasing)
                    {
                        if (player != null && Vector3.Distance(transform.position, player.GetTransform().position) <= currentChaseRadius)
                        {
                            SetChaseTarget();
                        }
                        else
                        {
                            StopChase(); // Прекратить преследование, если игрок находится за пределами радиуса обзора погони
                        }
                    }
                    else if (player != null && Vector3.Distance(transform.position, player.GetTransform().position) <= currentChaseRadius)
                    {
                        StartChase(); // Начать преследование, если игрок находится в пределах радиуса обзора погони
                    }

                    UpdateAnimationParameters();
                }*/
        restState.StartMove();
        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
        {
            findState.StartMove();
            findState.Update();
            idleTimer = 0;
        }
    }

    IEnumerator Rest()
    {
        isResting = true;
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;
        /*animator.SetBool("IsIdle", true);*/
        yield return new WaitForSeconds(idleTime);
        navMeshAgent.speed = walkingSpeed;
        isResting = false;
        navMeshAgent.isStopped = false;
        SetRandomDestination();
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection + transform.position, out hit, 20f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }

    void UpdateAnimationParameters()
    {
        float speed = navMeshAgent.speed;
        /*animator.SetBool("IsIdle", speed < idleSpeedThreshold && !isResting);
        animator.SetBool("IsWalking", speed >= idleSpeedThreshold && !isResting && !isChasing);
        animator.SetBool("IsRunning", speed >= runningSpeed * chaseThreshold && isChasing);*/
        animator.SetFloat("speed", speed);
    }
    void SetChaseTarget()
    {
        if (player != null)
            navMeshAgent.SetDestination(player.GetTransform().position);
    }
    public void StartChase()
    {
        isChasing = true;
        currentChaseRadius = extendedChaseRadius;
        navMeshAgent.speed = runningSpeed;
    }

    public void StopChase()
    {
        isChasing = false;
        currentChaseRadius = defaultChaseRadius;
        navMeshAgent.speed = walkingSpeed;
    }

    public void Dead() // Метод для смерти тролля
    {
        isDead = true;
        // Останавливаем анимацию или что-то еще, что нужно сделать перед уничтожением объекта
        Destroy(gameObject); // Уничтожаем объект тролля
    }


}
/*    public void DealDamage()
            {
                Vector3 attackRadius = new(5, 10, 3);

            }
*/