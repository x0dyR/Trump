using Trump.StateMachine;
using UnityEngine;

namespace Trump
{
    [RequireComponent(typeof(SphereCollider), typeof(ParticleSystem))]
    public class HealZone : MonoBehaviour
    {
        [field: SerializeField] private HealZoneConfig _config;
        private ParticleSystem _particles;
        private SphereCollider _collider;
        private float _cooldown = 0;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.radius = _config.Range;
            _particles = GetComponent<ParticleSystem>();
            var sh = _particles.shape;
            sh.scale = new Vector3(_config.Range, _config.Range, _config.Range);
        }

        private void OnTriggerEnter(Collider other)
        {
            _particles.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            _particles.Stop();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Debug.Log("Player in heal zone");
                if (character.TryGetComponent(out IHealth health))
                {
                    _cooldown += Time.deltaTime;
                    if (_cooldown >= _config.Cooldown)
                    {
                        health.Heal(_config.Heal);
                        _cooldown = 0;
                    }
                }
            }

        }
    }
}
