using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public interface INavAgent
    {
        Transform Transform { get; }
        NavMeshAgent NavAgent { get; }
    }
}
