using UnityEngine;

namespace Trump
{
    [CreateAssetMenu(fileName = "DamageZoneConfig", menuName = "Configs/DamageZoneConfig")]
    public class DamageZoneConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 100)] private float _damage;
        [field: SerializeField, Range(1, 100)] private float _cooldown;
        [field: SerializeField, Range(1, 25)] private float _range;
        [field:SerializeField] private ParticleSystem _particles;

        public float Damage => _damage;

        public float Cooldown => _cooldown;

        public float Range => _range;

        public ParticleSystem Particles => _particles;
    }
}
