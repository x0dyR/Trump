// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections;

// public class Troll : MonoBehaviour
// {
//     private Animator animator;
//     private NavMeshAgent navMeshAgent;
//     private GameObject player;
//     private float defaultChaseRadius = 10f;
//     private float extendedChaseRadius = 20f;
//     private float currentChaseRadius;
//     private bool isResting = false;
//     private bool isChasing = false;
//     public float chaseThreshold = 1.5f;
//     public float idleSpeedThreshold = 0.1f;
//     public float walkingSpeed = 2.5f;
//     public float runningSpeed = 5f;
//     public float idleTime = 5f;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         player = GameObject.FindWithTag("Player");
//         currentChaseRadius = defaultChaseRadius;
//         navMeshAgent.speed = walkingSpeed;
//         SetRandomDestination();
//     }

//     void Update()
// {
//     if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f && !isResting)
//         StartCoroutine(Rest());

//     if (isChasing)
//     {
//         if (player != null && Vector3.Distance(transform.position, player.transform.position) <= currentChaseRadius)
//         {
//             SetChaseTarget();
//         }
//         else
//         {
//             StopChase(); // Прекратить преследование, если игрок находится за пределами радиуса обзора погони
//         }
//     }
//     else if (player != null && Vector3.Distance(transform.position, player.transform.position) <= currentChaseRadius)
//     {
//         StartChase(); // Начать преследование, если игрок находится в пределах радиуса обзора погони
//     }

//     UpdateAnimationParameters();
// }



//     IEnumerator Rest()
//     {
//         isResting = true;
//         navMeshAgent.isStopped = true;
//         animator.SetBool("IsIdle", true);
//         yield return new WaitForSeconds(idleTime);
//         isResting = false;
//         navMeshAgent.isStopped = false;
//         SetRandomDestination();
//     }

//     void SetRandomDestination()
//     {
//         Vector3 randomDirection = Random.insideUnitSphere * 20f;
//         NavMeshHit hit;
//         NavMesh.SamplePosition(randomDirection + transform.position, out hit, 20f, NavMesh.AllAreas);
//         navMeshAgent.SetDestination(hit.position);
//     }

//     void SetChaseTarget()
//     {
//         if (player != null)
//             navMeshAgent.SetDestination(player.transform.position);
//     }

//     void UpdateAnimationParameters()
//     {
//         float speed = navMeshAgent.velocity.magnitude;
//         animator.SetBool("IsIdle", speed < idleSpeedThreshold && !isResting);
//         animator.SetBool("IsWalking", speed >= idleSpeedThreshold && !isResting && !isChasing);
//         animator.SetBool("IsRunning", speed >= runningSpeed * chaseThreshold && isChasing);
//         animator.SetFloat("speed", speed);
//     }

//     public void StartChase()
//     {
//         isChasing = true;
//         currentChaseRadius = extendedChaseRadius;
//         navMeshAgent.speed = runningSpeed;
//     }

//     public void StopChase()
//     {
//         isChasing = false;
//         currentChaseRadius = defaultChaseRadius;
//         navMeshAgent.speed = walkingSpeed;
//     }
// }

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;
using collegeGame;

// public class Troll : MonoBehaviour
// {
//     private Animator animator;
//     private NavMeshAgent navMeshAgent;
//     private GameObject player;
//     private float defaultChaseRadius = 10f;
//     private float extendedChaseRadius = 20f;
//     private float currentChaseRadius;
//     private bool isResting = false;
//     private bool isChasing = false;
//     private bool isDead = false; // Добавляем флаг для отслеживания смерти тролля
//     public float chaseThreshold = 1.5f;
//     public float idleSpeedThreshold = 0.1f;
//     public float walkingSpeed = 2.5f;
//     public float runningSpeed = 5f;
//     public float idleTime = 5f;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         player = GameObject.FindWithTag("Player");
//         currentChaseRadius = defaultChaseRadius;
//         navMeshAgent.speed = walkingSpeed;
//         SetRandomDestination();
//     }

//     void Update()
//     {
//         if (!isDead) // Проверяем, жив ли тролль, прежде чем выполнять его обновление
//         {
//             if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f && !isResting)
//                 StartCoroutine(Rest());

//             if (isChasing)
//             {
//                 if (player != null && Vector3.Distance(transform.position, player.transform.position) <= currentChaseRadius)
//                 {
//                     SetChaseTarget();
//                 }
//                 else
//                 {
//                     StopChase(); // Прекратить преследование, если игрок находится за пределами радиуса обзора погони
//                 }
//             }
//             else if (player != null && Vector3.Distance(transform.position, player.transform.position) <= currentChaseRadius)
//             {
//                 StartChase(); // Начать преследование, если игрок находится в пределах радиуса обзора погони
//             }

//             UpdateAnimationParameters();
//         }
//     }

//     IEnumerator Rest()
//     {
//         isResting = true;
//         navMeshAgent.isStopped = true;
//         animator.SetBool("IsIdle", true);
//         yield return new WaitForSeconds(idleTime);
//         isResting = false;
//         navMeshAgent.isStopped = false;
//         SetRandomDestination();
//     }

//     void SetRandomDestination()
//     {
//         Vector3 randomDirection = Random.insideUnitSphere * 20f;
//         NavMeshHit hit;
//         NavMesh.SamplePosition(randomDirection + transform.position, out hit, 20f, NavMesh.AllAreas);
//         navMeshAgent.SetDestination(hit.position);
//     }

//     void SetChaseTarget()
//     {
//         if (player != null)
//             navMeshAgent.SetDestination(player.transform.position);
//     }

//     void UpdateAnimationParameters()
//     {
//         float speed = navMeshAgent.velocity.magnitude;
//         animator.SetBool("IsIdle", speed < idleSpeedThreshold && !isResting);
//         animator.SetBool("IsWalking", speed >= idleSpeedThreshold && !isResting && !isChasing);
//         animator.SetBool("IsRunning", speed >= runningSpeed * chaseThreshold && isChasing);
//         animator.SetFloat("speed", speed);
//     }

//     public void StartChase()
//     {
//         isChasing = true;
//         currentChaseRadius = extendedChaseRadius;
//         navMeshAgent.speed = runningSpeed;
//     }

//     public void StopChase()
//     {
//         isChasing = false;
//         currentChaseRadius = defaultChaseRadius;
//         navMeshAgent.speed = walkingSpeed;
//     }


// }

public class Troll : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private ITarget player;
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
    public float idleTime = 5f;

    [Inject]
    private void Construct(ITarget target)
    {
        player = target;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentChaseRadius = defaultChaseRadius;
        navMeshAgent.speed = walkingSpeed;
        SetRandomDestination();
    }

    void Update()
    {
        if (!isDead) // Проверяем, жив ли тролль, прежде чем выполнять его обновление
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

    void SetChaseTarget()
    {
        if (player != null)
            navMeshAgent.SetDestination(player.GetTransform().position);
    }

    void UpdateAnimationParameters()
    {
        float speed = navMeshAgent.speed;
        /*animator.SetBool("IsIdle", speed < idleSpeedThreshold && !isResting);
        animator.SetBool("IsWalking", speed >= idleSpeedThreshold && !isResting && !isChasing);
        animator.SetBool("IsRunning", speed >= runningSpeed * chaseThreshold && isChasing);*/
        animator.SetFloat("speed", speed);
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


