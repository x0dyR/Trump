using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public interface INavAgent
    {
        Transform Transform { get; }
        float Speed { get; }
        NavMeshAgent NavAgent { get; }
    }
}
