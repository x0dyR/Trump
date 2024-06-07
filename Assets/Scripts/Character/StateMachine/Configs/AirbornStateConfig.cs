using System;
using UnityEngine;

namespace Trump
{
    [Serializable]
    public class AirbornStateConfig
    {
        [field: SerializeField] private JumpingStateConfig _jumpingStateConfig;
        [field: SerializeField] private float _speed;
        public JumpingStateConfig JumpingStateConfig => _jumpingStateConfig;
        public float Speed => _speed;

        public float BaseGravity => 2 * _jumpingStateConfig.MaxHeight / (_jumpingStateConfig.TimeToReachMaxHeight * _jumpingStateConfig.TimeToReachMaxHeight);
    }
}
