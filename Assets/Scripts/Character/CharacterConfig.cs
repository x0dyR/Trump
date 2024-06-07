using System;
using UnityEngine;

namespace Trump.StateMachine
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] private RunningStateConfig _runningStateConfig;
        [field: SerializeField] private AirbornStateConfig _airbornStateConfig;
        [field: SerializeField,Range(50,1000)] private float _health;

        public RunningStateConfig RunningStateConfig => _runningStateConfig;
        public AirbornStateConfig AirbornStateConfig => _airbornStateConfig;

        public float Health
        {
            get => _health;
            set
            {
                if (value < 1)
                {
                    _health = 0;
                    _health += 1;
                    throw new ArgumentOutOfRangeException("Health cant be below 1");
                }
                _health = value;
            }
        }
        private void OnValidate()
        {
            if (_health < 0)
            {
                _health = 0;
                _health += 1;
                throw new ArgumentOutOfRangeException("Health cant be below 1");
            }
        }
    }
}
