using System;
using UnityEngine;

namespace collegeGame
{
    [Serializable]
    public class JumpingStateConfig
    {
        [field: SerializeField, Range(0, 10)] private float _maxHeight;
        [field: SerializeField, Range(0, 10)] private float _timeToReachMaxHeight;

        public float StartYVelocity => 2 * _maxHeight / _timeToReachMaxHeight;
        public float MaxHeight => _maxHeight;
        public float TimeToReachMaxHeight => _timeToReachMaxHeight;
    }
}
