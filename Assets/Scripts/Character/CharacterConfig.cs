using UnityEngine;

namespace collegeGame.StateMachine
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] private RunningStateConfig _runningStateConfig;
        [field: SerializeField] private AirbornStateConfig _airbornStateConfig;

        public RunningStateConfig RunningStateConfig => _runningStateConfig;
        public AirbornStateConfig AirbornStateConfig => _airbornStateConfig;
    }
}
