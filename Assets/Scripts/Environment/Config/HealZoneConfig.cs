using UnityEngine;

namespace Trump
{
    [CreateAssetMenu(fileName = "HealZoneConfig", menuName = "Configs/HealZoneConfig")]
    public class HealZoneConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 100)] private float _heal;
        [field: SerializeField, Range(1, 100)] private float _cooldown;
        [field: SerializeField, Range(1, 25)] private float _range;
        [field: SerializeField] private ParticleSystem _particles;

        public float Heal => _heal;

        public float Cooldown => _cooldown;

        public float Range => _range;

        public ParticleSystem Particles => _particles;
    }
}
