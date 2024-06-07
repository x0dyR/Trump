using System;
using UnityEngine;
namespace Trump
{
    [Serializable]
    public class RunningStateConfig
    {
        [field: SerializeField, Range(5, 10)] public float Speed { get; private set; }
    }
}
